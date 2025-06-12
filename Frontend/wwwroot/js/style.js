document.addEventListener("DOMContentLoaded", function () {
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

    const loader = document.querySelector(".loader");
    const predictionResult = document.querySelector(".prediction-result");
    const appData = document.getElementById("app-data");
    window.hasInitialData = appData.dataset.hasInitialData === "true";

    const cleanUrlParameters = (essentialParams = []) => {
        const url = new URL(window.location.href);
        const currentParams = new Set(essentialParams);
        const allParamKeys = Array.from(url.searchParams.keys());
        allParamKeys.forEach(param => {
            if (!currentParams.has(param)) {
                url.searchParams.delete(param);
            }
        });
        window.history.replaceState({}, "", url);
    };

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

    //csv map 
    paths.forEach((path) => {
    path.addEventListener("click", function () {
      if (this.classList.contains("notFound")) {
        return;
        }
        predictionButton.disabled = true;

      const isActive = this.classList.contains("active");
      paths.forEach((p) => p.classList.remove("active"));
        if (isActive) {
            UkContainer.style.display = "none";
            predictionButton.disabled = !this.classList.contains("active");
            chinaContainer.style.display = "none";
          YearRadioButtons.forEach((radio) => (radio.disabled = true));
          AgeRadioButtons.forEach((radio) => (radio.disabled = true));
          GenderRadioButtons.forEach((radio) => (radio.disabled = true));
      } else {
        this.classList.add("active");
          YearRadioButtons.forEach((radio) => (radio.disabled = false));
          AgeRadioButtons.forEach((radio) => (radio.disabled = false));
          GenderRadioButtons.forEach((radio) => (radio.disabled = false));
            predictionButton.disabled = false;

        const titleElement = this.querySelector("title");
          if (titleElement) {
              const countryName = titleElement.textContent.trim();
              cleanUrlParameters();
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

    });
    });

    //js loader
    loader.classList.add("loader-hidden");
    if (!window.hasInitialData) {
        predictionResult.style.display = "none";
    } else {
        predictionResult.style.display = "block";
        predictionButton.disabled = true; 
    }

    const scroll = sessionStorage.getItem('scrollPosition');
    if (scroll) {
        window.scrollTo(0, parseInt(scroll));
        sessionStorage.removeItem('scrollPosition');
    }

    predictionButton.addEventListener("click", function (e) {
        e.preventDefault(); 
        loader.style.display = "flex";
        sessionStorage.setItem('scrollPosition', window.pageYOffset);
        loader.classList.remove("loader-hidden");
        predictionResult.style.display = "none";
        predictionButton.disabled = true;
        document.querySelector("form").submit();
    });    


    document.querySelector("form").addEventListener("submit", function (e) {
        const country = document.getElementById("Country").value;
        e.preventDefault();
        if (country !== "China" && country !== "United Kingdom") {
            document.getElementById("ChinaRegion").disabled = true;
            document.getElementById("UKRegion").disabled = true;
        }
        setTimeout(() => {
            this.submit();
        }, 0);
    });

    cleanUrlParameters();
   
});