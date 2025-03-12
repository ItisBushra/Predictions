document.addEventListener("DOMContentLoaded", function () {
  const paths = document.querySelectorAll("svg path");
  const radioButtons = document.querySelectorAll('input[type="radio"]');
  const predictionButton = document.querySelector(".button");
  const selectedCountryElement = document.getElementById("selected-country");
  const ChinaCountryElement = document.querySelector(
    ".container.china-container"
  );
  const UKCountryElement = document.querySelector(".container.uk-container");
  paths.forEach((path) => {
    path.addEventListener("click", function () {
      if (this.classList.contains("notFound")) {
        return;
      }

      const isActive = this.classList.contains("active");
      paths.forEach((p) => p.classList.remove("active"));
      if (isActive) {
        radioButtons.forEach((radio) => (radio.disabled = true));
        predictionButton.disabled = true;
        selectedCountryElement.textContent = "";
      } else {
        this.classList.add("active");
        radioButtons.forEach((radio) => (radio.disabled = false));
        predictionButton.disabled = false;
        const titleElement = this.querySelector("title");
        if (titleElement) {
          selectedCountryElement.textContent = titleElement.textContent;
        }
      }

      if (this.classList.contains("China") && !isActive) {
        ChinaCountryElement.style.display = "block";
      } else {
        ChinaCountryElement.style.display = "none";
      }

      if (this.classList.contains("Kingdom") && !isActive) {
        UKCountryElement.style.display = "block";
      } else {
        UKCountryElement.style.display = "none";
      }
    });
  });
});
