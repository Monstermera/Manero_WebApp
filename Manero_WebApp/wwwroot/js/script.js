//Shows and hides the serchbar.
function expand() {
    if (document.getElementById("navbar-search").className.includes("search-expanded")) {
        document.getElementById("navbar-search").classList.remove("search-expanded");
    }
    else {
        document.getElementById("navbar-search").classList.add("search-expanded");
    }
}

//Opens the mobile slideout menu
function openMobileMenu() {
    const menu = document.getElementById("navigation-slideout-menu");
    const menuBackground = document.getElementById("slideout-background");

    if (!menu.classList.contains("mobile-menu-open")) {
        menu.classList.add("mobile-menu-open");
        menuBackground.classList.add("slideout-open");
    } else {
        menu.classList.remove("mobile-menu-open");
        menuBackground.classList.remove("slideout-open");
    }
}

//Close the mobile slideout menu
function closeMobileMenu() {
    const menu = document.getElementById("navigation-slideout-menu");
    const menuBackground = document.getElementById("slideout-background");

    menu.classList.remove("mobile-menu-open");
    menuBackground.classList.remove("slideout-open");
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