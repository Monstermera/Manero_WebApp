function openSearch() {
    document.getElementById("myOverlay").style.display = "block";
}

function closeSearch() {
    document.getElementById("myOverlay").style.display = "none";
}

//Close Decision Modal
function closeDesicionModal() {
    const backdrop = document.getElementById("decision-modal-backdrop");
    const modal = document.getElementById("decision-pop-up-modal");

    backdrop.classList.add("opacity-0");
    modal.classList.add("decision-pop-up-modal-close");
    setTimeout(function () {
        modal.classList.add("d-none");
        backdrop.classList.add("d-none")
    }, 250);
}

//Open Decision Modal
function openDesicionModal() {
    const backdrop = document.getElementById("decision-modal-backdrop");
    const modal = document.getElementById("decision-pop-up-modal");

    modal.classList.remove("d-none");
    backdrop.classList.remove("d-none")
    modal.classList.remove("decision-pop-up-modal-close");
    
    setTimeout(function () {
        backdrop.classList.remove("opacity-0");       
    }, 50);
}

// Toggle password asterisks
function toggleVisibility() {
    var toggle = document.getElementById("password-input-icon");
    var passwordInput = document.getElementById("password");
    var confirmPasswordInput = document.getElementById("confirmPassword");
    if (toggle.className.includes("fa-eye-slash")) {
        toggle.classList.remove("fa-eye-slash");
        toggle.classList.add("fa-eye");
        passwordInput.type = "text";
        confirmPasswordInput.type = "text";
    }
    else {
        toggle.classList.remove("fa-eye");
        toggle.classList.add("fa-eye-slash");
        passwordInput.type = "password";
        confirmPasswordInput.type = "password";
    }
}



// Dropdowns
const filtersButton = document.getElementById("filters-button");
const filtersDropdown = document.getElementById("filters-dropdown");
const sortButton = document.getElementById("sort-by-button");
const sortDropdown = document.getElementById("sort-by-dropdown");

function toggleDropdown(dropdown) {
    if (dropdown.style.display === "block") {
        dropdown.style.display = "none";
    } else {
        dropdown.style.display = "block";
    }
}

filtersButton.addEventListener("click", function(event) {
    event.stopPropagation();
    toggleDropdown(filtersDropdown);

    sortDropdown.style.display = "none";
});


sortButton.addEventListener("click", function(event) {
    event.stopPropagation();
    toggleDropdown(sortDropdown);

    filtersDropdown.style.display = "none";
});

document.addEventListener("click", function(event) {
    filtersDropdown.style.display = "none";
    sortDropdown.style.display = "none";
});

