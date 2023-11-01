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
