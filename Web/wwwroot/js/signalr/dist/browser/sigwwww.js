"use strict"


var connection = new signalR.HubConnectionBuilder().
    withUrl("/abcd").
    build();
connection.on("plspls", function () {
    location.href = 'http://localhost:5017/Managers/ManageMovie/Index'
   
});
connection.on("addMovie", function () {
    location.href = 'http://localhost:5017/Customers/ListMovie'

});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
connection.on("cart", function () {
    location.href = 'http://localhost:5017/Customers/MyCart'
});
connection.on("RemoveItemFromCart", function () {
    location.href = 'http://localhost:5017/Customers/MyCart'
});
connection.on("Checkout", function () {
    location.href = 'http://localhost:5017/Customers/UserProfile'
});