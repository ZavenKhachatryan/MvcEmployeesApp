$(function () {
    $(document).on("keyup", "input[data-validate]", function () {
        validate();
    })
})

function validate() {
    var validPhone = /^(0[1-9]{2})([0-9]{6})$/;
    var validEmail = /^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$/;
    var validName = /^[a-zA-z]+$/;

    let isError = true;
    $("input[data-validate]").each(function () {
        $this = $(this);
        let attributes = $this.attr("data-validate").split(",");
        $.each(attributes, function (index, item) {
            $this.next(".validateMessage").html("")    
            if (item == "required") {
                if ($this.val() == "") {
                    $this.next(".validateMessage").html($this.attr("data-validate-name") + " Is Required")
                    isError = false;
                    return false;
                }
            }
            if (item == "email") {
                if (!$this.val().match(validEmail)) {
                    $this.next(".validateMessage").html($this.attr("data-validate-name") + " Is Invalid")
                    isError = false;
                }
            }
            if (item == "name") {
                if (!$this.val().match(validName)) {
                    $this.next(".validateMessage").html($this.attr("data-validate-name") + " Is Invalid")
                   
                    isError = false;
                }
            }
            if (item == "age") {
                if (isNaN(!$this.val()) || !$this.val().val < 1 || !$this.val().val > 101) {
                    $this.next(".validateMessage").html($this.attr("data-validate-name") + " Is Invalid")
                    isError = false;
                }
            }
            if (item == "phone") {
                if (!$this.val().match(validPhone)) {
                    $this.next(".validateMessage").html($this.attr("data-validate-name") + " Is Invalid")
                    isError = false;
                }
            }
        })
    })
    if (isError) {
        $("#editForm").submit();
    }
}