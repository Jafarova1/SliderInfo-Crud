$(document).ready(function () {


    //add basket

    $(document).on("submit", "#basket-form", function (e) {
        e.preventDefault();

        let productId = $(this).attr("data-id");

        let data = { id: productId };


        $ajax({

            url: "cart/addbasket",
            type: "Post",
            data: data,
            success: function (res) {
               $("sup.rounded-circle").text(res)
            }
     })




    })


    //delete product from basket
    $(document).on("submit", "#basket-delet-form", function (e) {
        e.preventDefault();

        let productId = $(this).attr("data-id");

        let data = { id: productId };


        $ajax({

            url: "cart/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $("sup.rounded-circle").text(res)
            }
        })




    })


})