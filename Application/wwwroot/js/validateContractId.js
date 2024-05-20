document.addEventListener("DOMContentLoaded", function () {
    const contractIdInput = document.getElementById("contractId");
    const contractDateInput = document.getElementById("contractDate");
    const submitButton = document.querySelector("button[type='submit']");
    const form = document.getElementById('contractForm');

    contractIdInput.addEventListener("input", validateForm);
    contractDateInput.addEventListener("input", validateForm);

    form.addEventListener('submit', function (event) {
        if (!validateForm()) {
            event.preventDefault();
        }
    });

    function validateForm() {
        let isValid = true;

        // Проверка идентификатора
        const contractId = contractIdInput.value;
        if (contractId === "" || isNaN(contractId) || parseInt(contractId) <= 0) {
            showError(contractIdInput, "ID должно быть числом больше 0");
            isValid = false;
        } else {
            clearError(contractIdInput);
        }

        // Проверка даты
        const contractDate = contractDateInput.value;
        if (contractDate === "") {
            showError(contractDateInput, "Дата не может быть пустой");
            isValid = false;
        } else {
            clearError(contractDateInput);
        }

        // Проверка существования ID
        if (isValid) {
            checkContractIdExists(contractId);
        }

        return isValid;
    }

    function showError(inputElement, message) {
        let errorElement = inputElement.nextElementSibling;
        if (!errorElement || !errorElement.classList.contains("error-message")) {
            errorElement = document.createElement("span");
            errorElement.className = "error-message";
            errorElement.style.color = "red";
            inputElement.parentNode.insertBefore(errorElement, inputElement.nextSibling);
        }
        errorElement.textContent = message;
        inputElement.classList.add("error");
    }

    function clearError(inputElement) {
        let errorElement = inputElement.nextElementSibling;
        if (errorElement && errorElement.classList.contains("error-message")) {
            errorElement.remove();
        }
        inputElement.classList.remove("error");
    }

    function checkContractIdExists(contractId) {
        fetch(`/Contract/CheckContractIdExists?id=${contractId}`)
            .then(response => response.json())
            .then(data => {
                if (data.exists) {
                    showError(contractIdInput, "ID уже существует");
                    submitButton.disabled = true;
                } else {
                    clearError(contractIdInput);
                    submitButton.disabled = false;
                }
            })
            .catch(error => {
                console.error("Ошибка при проверке ID:", error);
            });
    }
});