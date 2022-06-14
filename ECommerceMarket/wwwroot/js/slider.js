class SliderItem {
    constructor(filePath, order) {
        this.filePath = filePath;
        this.order = order;
    }
}

function slide(productId, direction) {
    if (productId) {
		$.ajax({
			type: "GET",
			url: "ProductPhotoView",
			async: false,
			data: {
				productId: productId,
				direction: direction
			},
			success: function (data, status, xhr) {
				//document.body.innerHTML = data;
				let photoContent = document.getElementById('photoContent');
				photoContent.outerHTML = data;
				console.log(data);
			}
		})
    }
}