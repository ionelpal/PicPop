$(function () {
    $(".AddToCart").click(function() {
        // Get the id from the link
        var id = $(this).attr("data-id");
        if (id != '') {
            // Perform the ajax post
            $.post("/ShoppingCart/Add", { "id": id },
                function(data) {
                    // Successful requests get here
                    $("#CartNumElements").parent().parent().show();
                    $("#CartNumElements").text(data.NumItems);
                });
        }
    });
});