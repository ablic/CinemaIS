var totalPrice = 0;
var seats = document.getElementsByClassName("seat");
var selectedTicketsContainer = document.getElementById("selected-tickets-container")

[].forEach.call(seats, seat => {
    seat.addEventListener("change", function () {
        let price = parseFloat(seat.getAttribute("data-price"))
        console.log("price: " + price);
        if (this.checked)
            totalPrice += price;
        else
            totalPrice -= price;

        console.log(totalPrice);
    })
})