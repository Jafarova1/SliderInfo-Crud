$(document).ready(function () {


 
    $(document).on("click", ".show - more - btn", function () {
       
        $ajax({

            url: `shop/showmore?skip=${5}`,
                type: "Get",
                success: function(res) {
                    console.log(res)
                }
        )}


    
})

