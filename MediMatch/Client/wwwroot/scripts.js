function initializeCalendar() {
    $(document).ready(function () {
        $('#calendar').fullCalendar({
            events: '/api/appointments', // API endpoint to fetch appointments
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            defaultView: 'month'
        });
    });
}