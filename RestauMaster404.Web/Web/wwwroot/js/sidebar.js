document.addEventListener('DOMContentLoaded', function () {
    // Obtener todos los botones que abren submenús (Platillos, Ventas)
    const subToggles = document.querySelectorAll('.sub-toggle');

    subToggles.forEach(toggle => {
        toggle.addEventListener('click', function (e) {
            e.preventDefault();

            // Alternar solo el submenú de este botón, sin cerrar los demás
            const currentSubmenu = this.nextElementSibling;
            currentSubmenu.classList.toggle('show');
        });
    });

    // Opcional: Controlar si se desea mostrar u ocultar el grupo CRUD completo
    const crudToggle = document.querySelector('.crud-toggle');
    if (crudToggle) {
        crudToggle.addEventListener('click', function (e) {
            e.preventDefault();
            const crudSubmenu = this.nextElementSibling;
            crudSubmenu.classList.toggle('show');
        });
    }
});
