document.addEventListener("DOMContentLoaded", function () {
    function applyFilters() {
        // Fuel type filter value (Lowercase)
        const selectedFuel = document.getElementById("fuel").value.toLowerCase();

        // Brand filter value (Lowercase)
        const selectedBrand = document.getElementById("brand").value.toLowerCase();

        // Year filter value
        const selectedYear = document.getElementById("year").value;

        // Get all car items
        const carItems = document.querySelectorAll(".car-item");

        // Loop through car items and apply the filters
        carItems.forEach((car) => {
            // Retrieve data attributes for each car item
            const carFuel = car.getAttribute("data-fuel").toLowerCase().trim();
            const carBrand = car.getAttribute("data-brand").toLowerCase().trim();
            const carYear = parseInt(car.getAttribute("data-year"), 10);

            // Debugging: Print out current filter values and data attributes
            console.log('Selected Fuel:', selectedFuel);
            console.log('Selected Brand:', selectedBrand);
            console.log('Selected Year:', selectedYear);
            console.log('Car Fuel:', carFuel);
            console.log('Car Brand:', carBrand);
            console.log('Car Year:', carYear);

            // Check if the car matches the selected filters
            const matchesFuel = selectedFuel === "all" || carFuel === selectedFuel;
            const matchesBrand = selectedBrand === "all" || carBrand === selectedBrand;
            const matchesYear = selectedYear === "all" || carYear >= parseInt(selectedYear, 10);

            // Show or hide based on the filter criteria
            if (matchesFuel && matchesBrand && matchesYear) {
                car.style.display = "block"; // Show
            } else {
                car.style.display = "none"; // Hide
            }
        });
    }

    // Assign the function to the button
    document.querySelector("button").addEventListener("click", applyFilters);
});
