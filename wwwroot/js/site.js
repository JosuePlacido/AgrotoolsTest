function removeField(e, index, id = null) {
	if (id) {
		document
			.querySelector("#questions-list")
			.insertAdjacentHTML(
				"afterbegin",
				`<input value="${id}"type="hidden" name="RemovedQuestions" />`
			);
	}
	for (index; index < questionCount; index++) {
		let item = document.querySelector("#question-" + index);
		item.setAttribute("name", `Questions[${index - 1}].Title`);
		item.setAttribute("id", `question-${index - 1}`);
		item.previousElementSibling.setAttribute("for", `${index - 1}`);
		item.nextElementSibling.setAttribute("data-valmsg-for", index - 1);
		item.nextElementSibling.nextElementSibling.setAttribute(
			"onclick",
			`removeField(this,${index - 1})`
		);
	}
	e.parentNode.remove();
	questionCount--;
}
function generateTemplate(qtd) {
	return `
<div class="mb-3 border-bottom border-1 border-dark p-2">
	<label for="${qtd}" class="control-label">Questão
	</label>
	<textarea id="question-${qtd}" name="Questions[${qtd}].Title"
	data-val="true" data-val-required="Campo obrigatório"
	class="form-control" rows="2"></textarea>
	<span class="text-danger field-validation-error" data-valmsg-for="Questions[${qtd}].Title"
		data-valmsg-replace="true"></span>
	<button type="button" onclick="removeField(this,${qtd});" class="mt-2 btn-remove btn btn-danger">
		Remover
	</button>
</div>
`;
}
let questionCount = parseInt(document.querySelector("#question-count")?.value);
document.querySelector("#btn-add-question")?.addEventListener("click", () => {
	document
		.querySelector("#questions-list")
		.insertAdjacentHTML("beforeend", generateTemplate(questionCount));
	questionCount++;
});

function validateRequiredFields() {
	document.querySelectorAll('[data-val="true"]').forEach((item, index) => {
		item.classList.remove("input-validation-error");
		item.nextElementSibling.innerHTML = "";
		if (!item.value) {
			item.classList.add("input-validation-error");
			item.nextElementSibling.append(
				item.getAttribute("data-val-required")
			);
		}
	});
}
document.querySelector("#form")?.addEventListener("submit", (evt) => {
	validateRequiredFields();
	document.querySelectorAll(".input-validation-error").length > 0 &&
		evt.preventDefault();
});

document.querySelector("#form-respond")?.addEventListener("submit", (evt) => {
	validateRequiredFields();
	validateLocation();
	document.querySelectorAll(".input-validation-error").length > 0 &&
		evt.preventDefault();
});
document.querySelector("#btn-prev")?.addEventListener("click", (ev) => {
	const currentItem = document.querySelector(".question-item.active");
	if (currentItem.previousElementSibling.tagName === "ARTICLE") {
		const currentIndicator = document.querySelector(".current");
		const question = document.querySelector(
			".question-item.active>textarea"
		);
		if (question.value) {
			currentIndicator.classList.add("done");
		} else {
			currentIndicator.classList.remove("done");
		}
		currentIndicator.classList.remove("current");
		currentIndicator.previousElementSibling.classList.add("current");
		currentItem.classList.remove("active");
		currentItem.previousElementSibling.classList.add("active");
	}
});

document.querySelector("#btn-next")?.addEventListener("click", (ev) => {
	const currentItem = document.querySelector(".question-item.active");
	if (currentItem.nextElementSibling.tagName === "ARTICLE") {
		const currentIndicator = document.querySelector(".current");
		const question = document.querySelector(
			".question-item.active>textarea"
		);
		if (question.value) {
			currentIndicator.classList.add("done");
		} else {
			currentIndicator.classList.remove("done");
		}
		currentIndicator.classList.remove("current");
		currentIndicator.nextElementSibling.classList.add("current");
		currentItem.classList.remove("active");
		currentItem.nextElementSibling.classList.add("active");
	}
});
function inrange(min, number, max) {
	if (number != "" && !isNaN(number) && number >= min && number <= max) {
		return true;
	} else {
		return false;
	}
}
function validateLocation() {
	document.querySelectorAll('[name="Lat"]').forEach((latitude) => {
		latitude.classList.remove("input-validation-error");
		latitude.nextElementSibling.innerHTML = "";
		if (!inrange(-90, latitude.value, 90)) {
			latitude.classList.add("input-validation-error");
			latitude.nextElementSibling.append("Coordenada inválida");
		}
	});
	document.querySelectorAll('[name="Long"]').forEach((longitude) => {
		longitude.classList.remove("input-validation-error");
		longitude.nextElementSibling.innerHTML = "";
		if (!inrange(-180, longitude.value, 180)) {
			longitude.classList.add("input-validation-error");
			longitude.nextElementSibling.append("Coordenada inválida");
		}
	});
}
