document.addEventListener("DOMContentLoaded", function () {// must be DRY
  const paths = document.querySelectorAll("svg path");
    const YearRadioButtons = document.querySelectorAll('input[name="Year"]');
    const AgeRadioButtons = document.querySelectorAll('#Age input[type="radio"]');
    const GenderRadioButtons = document.querySelectorAll('#Gender input[type="radio"]');
    const ChinaRegionRadioButtons = document.querySelectorAll('#China input[type="radio"]');
    const UKRegionRadioButtons = document.querySelectorAll('#UK input[type="radio"]');
    const SelectedGenderInput = document.getElementById('SelecetedGender');
    const SelectedAgeInput = document.getElementById('SelecetedAge');
    const predictionButton = document.querySelector(".button");
    const selectedCountryElement = document.getElementById("selected-country");
    const countryInput = document.getElementById("Country");
    const chinaContainer = document.getElementById("China");
    const UkContainer = document.getElementById("UK");

    ChinaRegionRadioButtons.forEach(radio => {
        radio.addEventListener('change', function () {
            const selectedRegion = this.value;
            countryInput.value = selectedRegion;
            selectedCountryElement.textContent = selectedRegion;
        });
    });
    UKRegionRadioButtons.forEach(radio => {
        radio.addEventListener('change', function () {
            const selectedRegion = this.value;
            countryInput.value = selectedRegion;
            selectedCountryElement.textContent = selectedRegion;
        });
    });

    //csv map 
    paths.forEach((path) => {
    path.addEventListener("click", function () {
      if (this.classList.contains("notFound")) {
        return;
        }

      const isActive = this.classList.contains("active");
      paths.forEach((p) => p.classList.remove("active"));
        if (isActive) {
            UkContainer.style.display = "none";
            chinaContainer.style.display = "none";
          YearRadioButtons.forEach((radio) => (radio.disabled = true));
          AgeRadioButtons.forEach((radio) => (radio.disabled = true));
          GenderRadioButtons.forEach((radio) => (radio.disabled = true));
          predictionButton.disabled = true;
          selectedCountryElement.textContent = "";
          countryInput.value = "";
      } else {
        this.classList.add("active");
          YearRadioButtons.forEach((radio) => (radio.disabled = false));
          AgeRadioButtons.forEach((radio) => (radio.disabled = false));
          GenderRadioButtons.forEach((radio) => (radio.disabled = false));
        predictionButton.disabled = false;
        const titleElement = this.querySelector("title");
          if (titleElement) {
                  const countryName = titleElement.textContent;
                  selectedCountryElement.textContent = countryName;
                  countryInput.value = countryName;
          }
          if (titleElement.textContent.includes("China")) {
              chinaContainer.style.display = "block";
              UkContainer.style.display = "none";
              const selectedChinaRegion = document.querySelector('#China input[type="radio"]:checked');
              if (selectedChinaRegion) {
                  countryInput.value = selectedChinaRegion.value;
                  selectedCountryElement.textContent = selectedChinaRegion.value;
              }
          }
          if (titleElement.textContent.includes("United Kingdom") && !titleElement.textContent.includes("China")) {
              UkContainer.style.display = "block";
              chinaContainer.style.display = "none";
              const selectedUkRegion = document.querySelector('#UK input[type="radio"]:checked');
              if (selectedUkRegion) {
                  countryInput.value = selectedUkRegion.value;
                  selectedCountryElement.textContent = selectedUkRegion.value;
              }
          }
          if (!titleElement.textContent.includes("United Kingdom") && !titleElement.textContent.includes("China")) {
              chinaContainer.style.display = "none";
              UkContainer.style.display = "none";
          }
        }

        //radio age and gender
        GenderRadioButtons.forEach(radio => {
            radio.addEventListener('change', function () {
                const selectedGenderId = this.id;
                SelectedGenderInput.name = selectedGenderId;
            });
        });
        AgeRadioButtons.forEach(radio => {
            radio.addEventListener('change', function () {
                const selectedAgeId = this.id;
                SelectedAgeInput.name = selectedAgeId;
            });
        });

        document.addEventListener('DOMContentLoaded', function () {
            const initiallyCheckedRadioGender = document.querySelector('#Gender input[type="radio"]:checked');
            const initiallyCheckedRadioAge = document.querySelector('#Age input[type="radio"]:checked');
            if (initiallyCheckedRadioGender && initiallyCheckedRadioAge) {
                SelectedGenderInput.value = initiallyCheckedRadioGender.id;
                SelectedAgeInput.value = initiallyCheckedRadioAge.id;
            }
        });
    });
  });
});