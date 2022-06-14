function viewProduct(productId) {
	if (productId) {
		/*$.ajax({
			type: "GET",
			url: "ProductView/ProductPage",
			async: false,
			data: {
				ProductId: productId
			},
			success: function (data, status, xhr) {
				//document.body.innerHTML = data;
				window.location.href = data.redirect;
			}
		})*/

		//let url = "@Url.Action('ProductView', 'ProductPage')" + "?productId=" + productId;
		let url = 'ProductView/ProductPage' + '?productId=' + productId;
		window.location.href = url;
    }	
}