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