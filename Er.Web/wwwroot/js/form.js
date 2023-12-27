document.addEventListener("DOMContentLoaded", function () {
    // register the required buttons when the form loads
    FormManager.registerNavigationButtons();
    FormManager.registerUploadTriggers();
    FormManager.registerSubmissionButton();
});

FormManager = {
    uploadFile: function (file) {
        // serialize the form data
        let form = document.querySelector("form");
        let formData = new FormData(form);

        // adds the uploaded file to the serialized form
        formData.append(file.name, file);

        // send the request
        let response = HttpClient.post("/Rebate/Upload", formData);

        if (!response.isSuccess) {
            // TODO: output error message to the user
            console.log(response.message);
        }

        // TODO: output a success message to the user
        // renders the partial view from controller
        $("#form-wrapper").html(response.data);
        $("form").data("validator", null);
        $.validator.unobtrusive.parse("form");

        // TODO: detach old event listeners to prevent memory leaks
        // button events no longer work - re-registers the events
        FormManager.registerNavigationButtons();
        FormManager.registerUploadTriggers();
        FormManager.registerSubmissionButton();
    },

    submitForm: function () {
        // validates the form using model data attributes
        var $form = $("form");

        if (!$form.valid()) {
            $form.validate();

            return;
        }

        // serialize the form data
        let form = document.querySelector("form");
        let formData = new FormData(form);

        // send the request
        let response = HttpClient.post("/Rebate/Submit", formData);

        if (!response.isSuccess) {
            // TODO: output error message to the user
            console.log(response.message);
        }

        // renders the partial view from controller
        $("#form-wrapper").html(response.data);
        $("form").data("validator", null);
        $.validator.unobtrusive.parse("form");

        // TODO: detach old event listeners to prevent memory leaks
        // button events no longer work - re-registers the events
        FormManager.registerNavigationButtons();
        FormManager.registerUploadTriggers();
        FormManager.registerSubmissionButton();
    },

    registerNavigationButtons: function () {
        // retrieve the next and back buttons
        let nextButtons = document.querySelectorAll(".form-navigation .next");
        let backButtons = document.querySelectorAll(".form-navigation .back");

        for (let i = 0; i < nextButtons.length; i++) {
            // attach an event listener to navigate forward
            nextButtons[i].addEventListener("click", function (event) {
                let currentStep = event.target.closest(".step").dataset.step;
                let stepToNavigateTo = event.target.dataset.next;

                NavigationManager.navigateForward(currentStep, stepToNavigateTo);
            });
        }

        for (let i = 0; i < backButtons.length; i++) {
            // attach an event listener to navigate backward
            backButtons[i].addEventListener("click", function (event) {
                let currentStep = event.target.closest(".step").dataset.step;
                let stepToNavigateTo = event.target.dataset.back;

                NavigationManager.navigateBackward(currentStep, stepToNavigateTo);
            });
        }
    },

    registerUploadTriggers: function () {
        // retrieve the file inputs
        let uploadTriggers = document.querySelectorAll(".form-file-upload");

        for (let i = 0; i < uploadTriggers.length; i++) {
            // attach an event listener to upload the file
            uploadTriggers[i].addEventListener("change", function (event) {
                let file = event.target.files[0];
                FormManager.uploadFile(file);
            });
        }
    },

    registerSubmissionButton: function () {
        let form = document.querySelector("form");

        // attach an event listener on submission to submit the form
        form.addEventListener("submit", function (event) {
            event.preventDefault();
            FormManager.submitForm();
        });
    }
}

NavigationManager = {
    navigateForward: function (currentStep, stepToNavigateTo) {
        // validates the form using model data attributes
        var $form = $("form");

        console.log($form.validate());

        if (!$form.valid()) {
            $form.validate();

            return;
        }

        // retrieves the step containers for hiding/showing 
        let currentStepContainer = document.querySelector(`.step[data-step="${currentStep}"]`);
        let nextStepContainer = document.querySelector(`.step[data-step="${stepToNavigateTo}"]`);

        if (!nextStepContainer) {
            console.log("Failed to navigate forward, you should try again");

            return;
        }

        // hide/show appropriate steps
        currentStepContainer.style.display = "none";
        nextStepContainer.style.display = "block";
    },

    navigateBackward: function (currentStep, stepToNavigateTo) {
        // retrieves the step containers for hiding/showing
        let currentStepContainer = document.querySelector(`.step[data-step="${currentStep}"]`);
        let previousStepContainer = document.querySelector(`.step[data-step="${stepToNavigateTo}"]`);

        if (!previousStepContainer) {
            console.log("Failed to navigate backward, you should try again");

            return;
        }

        // hide/show appropriate steps
        currentStepContainer.style.display = "none";
        previousStepContainer.style.display = "block";
    }
}

HttpClient = {
    post: function (url, data) {
        // create the response structure
        let response = {
            isSuccess: false,
            data: null,
            message: null
        };

        // sends a request to an endpoint
        try {
            $.ajax({
                url: url,
                type: "POST",
                data: data,
                async: false,
                processData: false,
                contentType: false,
                success: function (result) {
                    response.isSuccess = true;
                    response.data = result;
                },
                error: function (result) {
                    console.log("An error occurred in the server: " + result);

                    response.isSuccess = false;
                    response.message = result.message;
                }
            });
        } catch (error) {
            console.log("An unexpected error occurred processing data: " + error);

            response.isSuccess = false;
            response.message = error.message;
        }

        return response;
    }
}