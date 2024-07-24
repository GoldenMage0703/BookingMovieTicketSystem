// Chart for Payments by Theater
var ctxTheater = document.getElementById('theaterPaymentsChart').getContext('2d');
var theaterPaymentsChart = new Chart(ctxTheater, {
    type: 'bar',
    data: {
        labels: @Html.Raw(Model.TheaterLabelsJson),
        datasets: [{
            label: 'Total Payments by Theater',
            data: @Html.Raw(Model.TheaterPaymentsJson),
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderColor: 'rgba(75, 192, 192, 1)',
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});

// Chart for Payments by Movie
var ctxMovie = document.getElementById('moviePaymentsChart').getContext('2d');
var moviePaymentsChart = new Chart(ctxMovie, {
    type: 'bar',
    data: {
        labels: @Html.Raw(Model.MovieLabelsJson),
        datasets: [{
            label: 'Total Payments by Movie',
            data: @Html.Raw(Model.MoviePaymentsJson),
            backgroundColor: 'rgba(153, 102, 255, 0.2)',
            borderColor: 'rgba(153, 102, 255, 1)',
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});