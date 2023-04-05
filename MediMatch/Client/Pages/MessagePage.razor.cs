using MediMatch.Client.Pages;
using MediMatch.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace MediMatch.Client.Pages
{
    
    public partial class MessagePage
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        private List<ApplicationUserDto> Users { get; set; } = new List<ApplicationUserDto>();
        private ApplicationUserDto UserTexting { get; set; } = new ApplicationUserDto();
        private List<Message> Messages { get; set; } = new List<Message>();
        private string newMessageText = "";
        private string MyUserId { get; set; }
        /*
         * private ApplicationUserDto myAppUser { get; set; } = new ApplicationUserDto();
        private string newMessageText = "";
        private List<Message> messages = new List<Message>();
        private string currentUserID;
        private List<(Message message, ApplicationUserDto sender)> messagesWithSenders;
        private List<Message> messagesForSelectedUser = new List<Message>();
        */
        protected override async Task OnInitializedAsync()
        {
            Users = await Http.GetFromJsonAsync<List<ApplicationUserDto>>("api/message/get-users");
            try
            {
                MyUserId = await Http.GetFromJsonAsync<string>("api/message/get-my-id");
                string h = "h";
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        private async Task SelectTextUser(ApplicationUserDto myUser)
        {
            try
            {
                UserTexting = myUser;
                Messages = await Http.GetFromJsonAsync<List<Message>>("api/message/get-messages/" + UserTexting.Id);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }
        
        private async Task FetchMessages()
        {
            try
            {
                Messages = await Http.GetFromJsonAsync<List<Message>>("api/message/get-messages/" + UserTexting.Id);
            }catch(Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        // send message method 
        private async Task SendMessage()
        {
            if (!string.IsNullOrEmpty(newMessageText))
            {
                if (UserTexting == null)
                {
                    Console.WriteLine("Error: currentUserID is empty or null");
                    return;
                }

                Message newMessage = new Message
                {
                    MessageTxt = newMessageText,
                    MessageDate = DateTime.Now,
                    MessageFromID = MyUserId,
                    MessageToID = UserTexting.Id
                };
    
                await Http.PostAsJsonAsync("api/message/send-message", newMessage);
                Messages = await Http.GetFromJsonAsync<List<Message>>($"api/message/get-messages/" + UserTexting.Id);
                newMessageText = "";
                StateHasChanged();
            }
        }
        /*
        private async void SelectUser(string userId)
        {
            selectedUser = userId;
            await FetchMessagesForSelectedUser();
        }
        // method to get current user/ sender name 
        private async Task<string> GetCurrentUserAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User.Identity;
            Console.WriteLine(user.Name);

            return user.Name;


        }
        // get user id method 
        private async Task<ApplicationUserDto> GetUserByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            try
            {
                var user = await Http.GetFromJsonAsync<ApplicationUserDto>($"api/User/GetUserById/{userId}");
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user: {ex.Message}");
                return null;
            }
        }
        // get messages for users method 
        private async Task<List<(Message message, ApplicationUserDto sender)>> GetMessagesForUserAsync(string selectedUser)
        {
            if (string.IsNullOrEmpty(selectedUser))
            {
                return new List<(Message message, ApplicationUserDto sender)>();
            }

            var messagesForUser = messages.Where(m => m.MessageFromID == selectedUser || m.MessageToID == selectedUser).ToList();
            var messagesWithSenders = new List<(Message message, ApplicationUserDto sender)>();

            foreach (var message in messagesForUser)
            {
                var sender = await GetUserByIdAsync(message.MessageFromID);
                messagesWithSenders.Add((message, sender));
            }

            return messagesWithSenders;
        }
        //fetch messages for selected users method 
        private async Task FetchMessagesForSelectedUser()
        {
            if (!string.IsNullOrEmpty(selectedUser))
            {
                messagesForSelectedUser = await Http.GetFromJsonAsync<List<Message>>($"api/GetMessagesBetweenUsers/{currentUserID}/{selectedUser}");
                StateHasChanged();
            }
        }
        */
    }
}

