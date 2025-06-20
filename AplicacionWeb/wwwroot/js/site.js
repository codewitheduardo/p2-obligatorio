// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    const sidebar = document.getElementById("sidebar-menu");
    const toggleButton = document.getElementById("menu-toggle");
    const closeButton = document.getElementById("close-sidebar");

    toggleButton.addEventListener("click", function () {
        sidebar.style.transform = "translateX(0)";
        document.body.classList.add("sidebar-open");
    });

    closeButton.addEventListener("click", function () {
        sidebar.style.transform = "translateX(-100%)";
        document.body.classList.remove("sidebar-open");
    });

    document.addEventListener("click", function (event) {
        const isClickInsideSidebar = sidebar.contains(event.target);
        const isClickOnToggle = toggleButton.contains(event.target);

        if (!isClickInsideSidebar && !isClickOnToggle) {
            sidebar.style.transform = "translateX(-100%)";
            document.body.classList.remove("sidebar-open");
        }
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const formContainer = document.getElementById("searchFormContainer");
    const toggleButton = document.getElementById("toggleFormBtn");
    const form = formContainer.querySelector("form");

    toggleButton.addEventListener("click", function (event) {
        event.stopPropagation(); // Evita que el clic se propague y cierre el formulario inmediatamente
        if (formContainer.classList.contains("hidden")) {
            formContainer.classList.remove("hidden");
        } else {
            formContainer.classList.add("hidden");
        }
    });

    document.addEventListener("click", function (event) {
        const isClickInsideForm = formContainer.contains(event.target);
        const isClickOnToggle = toggleButton.contains(event.target);

        if (!isClickInsideForm && !isClickOnToggle) {
            formContainer.classList.add("hidden");
            form.reset(); // Limpia todos los campos del formulario
        }
    });
});