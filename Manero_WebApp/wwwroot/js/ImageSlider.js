const imageBody = document.querySelectorAll(".image-component-body");
const pageDots = document.querySelector(".page-dots");

let currentIndex = 0;

function showImageBody(index) {
    imageBody.forEach((imageBody, i) => {
        if (i >= index && i < index + 1) {
            imageBody.classList.remove("d-none");
            imageBody.classList.add("d-block");
        } else {
            imageBody.classList.add("d-none");
            imageBody.classList.remove("d-block");
        }
    });
}

function createPageDots() {
    const numPages = Math.ceil(imageBody.length / 1);
    for (let i = 0; i < numPages; i++) {
        const dot = document.createElement("span");
        dot.classList.add("page-dot");
        dot.addEventListener("click", () => {
            currentIndex = i * 1;
            showImageBody(currentIndex);
            updatePageDots();
        });
        pageDots.appendChild(dot);
    }
}

function updatePageDots() {
    const dots = document.querySelectorAll(".page-dot");
    dots.forEach((dot, i) => {
        if (i * 1 === currentIndex) {
            dot.classList.add("active");
        } else {
            dot.classList.remove("active");
        }
    });
}

createPageDots();
showImageBody(currentIndex);
updatePageDots();