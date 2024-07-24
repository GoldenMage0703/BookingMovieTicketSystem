async function loadSeats() {
    const showtimeId = document.getElementById("date").value;
    const response = await fetch(`/Customers/SeatSelected?handler=GetSeats&showtimeId=${showtimeId}`);
    const seats = await response.json();

    const seatContainer = document.getElementById("seat-container");
    seatContainer.innerHTML = '';

    const totalSeats = seats.length;
    const seatsPerRow = 8; // Adjust this value based on your layout

    for (let i = 0; i < totalSeats / seatsPerRow; i++) {
        const row = document.createElement("div");
        row.className = "row justify-content-center mb-2";

        for (let j = 0; j < seatsPerRow; j++) {
            const index = i * seatsPerRow + j;
            if (index < totalSeats) {
                const seat = seats[index];
                const seatDiv = document.createElement("div");
                seatDiv.id = `seat-${index}`;
                seatDiv.className = `seat ${seat.isBooking ? 'occupied' : ''}`;
                seatDiv.innerText = `${seat.rowNum}${seat.seatNum}`;

                row.appendChild(seatDiv);
            }
        }

        seatContainer.appendChild(row);
    }
}

// Initial load
document.addEventListener("DOMContentLoaded", loadSeats);

// Add event listeners to update selected seats
document.addEventListener('DOMContentLoaded', (event) => {
    const seats = document.querySelectorAll('.seat');
    seats.forEach(seat => {
        seat.addEventListener('click', () => {
            seat.classList.toggle('selected');
            updateSelectedCount();
        });
    });

    function updateSelectedCount() {
        const selectedSeats = document.querySelectorAll('.seat.selected');
        const count = selectedSeats.length;
        document.getElementById('count').innerText = count;
        document.getElementById('total').innerText = count * 10; // Assuming each seat costs $10
    }
});
