// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// wwwroot/js/site.js

document.addEventListener("DOMContentLoaded", function () {
    const themeToggleButton = document.getElementById("theme-toggle");
    const body = document.body;

    themeToggleButton.addEventListener("click", function () {
        body.classList.toggle("dark-theme");
        if (body.classList.contains("dark-theme")) {
            localStorage.setItem("theme", "dark");
        } else {
            localStorage.setItem("theme", "light");
        }
    });

    if (localStorage.getItem("theme") === "dark") {
        body.classList.add("dark-theme");
    }
});
