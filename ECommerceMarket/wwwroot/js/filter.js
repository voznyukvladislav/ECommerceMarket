function checkAndSend(checkboxId, presetId, attributeId, attributeValue) {
    check(checkboxId);

    sendFilter(presetId, attributeId, attributeValue);
}

function sendFilter(presetId, attributeId, attributeValue) {
    if (presetId && attributeId && attributeValue) {
		$.ajax({
			type: "POST",
			url: "ProductView/Filter",
			async: false,
			data: {
				PresetId: presetId
				AttributeId: attributeId,
				attributeValue: attributeValue
			},
			success: () => {
				document.location.reload();
			}
		})
    }
}