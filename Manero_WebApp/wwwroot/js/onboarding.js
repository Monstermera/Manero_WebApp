document.addEventListener("DOMContentLoaded", function () {
    const onboardingSection = document.querySelector(".onboarding");
    const welcomeSection = document.querySelector(".welcome");

    welcomeSection.classList.add("loading");

    setTimeout(function () {
        welcomeSection.style.display = "none";
        onboardingSection.style.display = "block";
    }, 3000); 
});

(document).getElementById('confirmButton').addEventListener('click', function () {
    document.querySelector('.page1').style.display = 'none';
    document.querySelector('.page2').style.display = 'block';
}, 5000);