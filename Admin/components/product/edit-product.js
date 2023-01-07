var url = 'https://localhost:7000/api/AdminProduct';

var productInfo = [];
var id = null;

function getProductInfo() {
    const urlParams = new URLSearchParams(window.location.search);
    id = urlParams.get('id');
    const getProductInfoUrl = url + "/GetProduct" + id;
    axios.get(getProductInfoUrl)
        .then(res => {
            if (res.status === 200) {
                productInfo = res.data.data;
                console.log(productInfo);
                document.getElementById('productImg').src = productInfo.imageProductPath;
                document.getElementById('file-name').textContent = productInfo.imageProductName;
                //document.querySelector('[name="code"]').value = productInfo.code;
                document.querySelector('[name="name"]').value = productInfo.name;
                document.querySelector('[name="intro"]').value = productInfo.intro;
                document.querySelector('[name="categoryId"]').value = productInfo.categoryId;
                document.querySelector('[name="manufacturerId"]').value = productInfo.manufacturerId;
                document.querySelector('[name="authorId"]').value = productInfo.authorId;
                document.querySelector('[name="price"]').value = productInfo.price;
                document.querySelector('[name="quantity"]').value = productInfo.quantity;
                document.querySelector('[name="slug"]').value = productInfo.slug;
                document.querySelector('[name="status"]').value = productInfo.status;
                document.getElementById('productDescription').childNodes[0].childNodes[0].outerHTML = productInfo.description
            }
        })
};

function editProduct() {
    const image = document.getElementById('productImg').src;
    const imgName = document.getElementById('file-name').textContent;
    //const code1 = document.getElementById('code1').textContent;
    //const code2 = document.getElementById('code2').textContent;
    //const code = code1 + code2 + document.querySelector('[name="code"]').value;
    const name = document.querySelector('[name="name"]').value;
    const intro = document.querySelector('[name="intro"]').value;
    const categoryId = JSON.parse(document.getElementById('categoryId').value.split(',')[0]);
    const manufacturerId = JSON.parse(document.getElementById('manufacturerId').value.split(',')[0]);
    const authorId = JSON.parse(document.getElementById('authorId').value);
    const price = document.getElementById('productPrice').value;
    const quantity = document.getElementById('productQuantity').value;
    const slug = document.querySelector('[name="slug"]').value;
    const status = document.querySelector('[name="status"]').value;
    const description = document.getElementById('productDescription').childNodes[0].childNodes[0].outerHTML;

    productInfo.imageProductPath = image;
    productInfo.imageProductName = imgName;
    //productInfo.code = code;
    productInfo.name = name;
    productInfo.intro = intro;
    productInfo.categoryId = +categoryId;
    productInfo.manufacturerId = +manufacturerId;
    productInfo.authorId = +authorId;
    productInfo.price = +price;
    productInfo.quantity = +quantity;
    productInfo.slug = slug;
    productInfo.status = status;
    productInfo.description = description;

    const editProductInfoUrl = url + "/" + id;
    axios.put(editProductInfoUrl, productInfo)
        .then(res => {
            if (res.status === 200) {
                window.location.href = "product.html";
            }
        })
    return false;
};

function productCode1(e) {
    var valueTwo = $('#categoryId').val().split(',')[1];
    document.getElementById("code1").innerHTML = valueTwo;

    /*
    if (valueTwo == "C2") {
        var content = `
            <label for="authorId" class="font-weight-semibold">Author</label>
            <select id="authorId" class="form-control" name="authorId" placeholder="Author">
            </select>
        `;
        document.querySelector('#test').innerHTML = content;
    }
    */
};

function productCode2(e) {
    var valueTwo = $('#manufacturerId').val().split(',')[1];
    document.getElementById("code2").innerHTML = valueTwo
};

axios.get('https://localhost:7000/api/AdminCategory?status=0&pageSize=1000')
    .then(response => {
        if (response.status === 200) {
            document.querySelector('#categoryId').innerHTML = '';
            var categories = response.data.data;
            var content = ``;
            var empty = `<option></option>`;
            categories.map(category => {
                content += `<option value="${category.id},${category.code}">${category.name}</option>`;
            });
            document.querySelector('#categoryId').innerHTML = empty + content;
        }
    });

axios.get('https://localhost:7000/api/AdminManufacturer?pageSize=1000')
    .then(response => {
        if (response.status === 200) {
            document.querySelector('#manufacturerId').innerHTML = '';
            var manufacturers = response.data.data.manufacturers;
            var content = ``;
            var empty = `<option></option>`;
            manufacturers.map(manufacturer => {
                content += `<option value="${manufacturer.id},${manufacturer.code}">${manufacturer.name}</option>`;
            });
            document.querySelector('#manufacturerId').innerHTML = empty + content;
        }
    });

axios.get('https://localhost:7000/api/AdminAuthor?pageSize=1000')
    .then(response => {
        if (response.status === 200) {
            document.querySelector('#authorId').innerHTML = '';
            var authors = response.data.data.authors;
            var content = ``;
            var empty = `<option></option>`;
            authors.map(author => {
                content += `<option value="${author.id}">${author.name}</option>`;
            });
            document.querySelector('#authorId').innerHTML = empty + content;
        }
    });

$("#form-validation").validate({
    errorElement: 'div',
    errorClass: 'is-invalid',
    validClass: 'is-valid',
    rules: {
        code: {
            required: true,
            rangelength: [2, 2]
        },
        name: {
            required: true
        },
        categoryId: {
            required: true
        },
        manufacturerId: {
            required: true
        },
        price: {
            required: true,
            number: true
        },
        quantity: {
            required: true,
            number: true
        },
        slug: {
            required: true
        },
        status: {
            required: true
        },

    },
    messages: {
        code: {
            rangelength: "Enter 2 characters"
        },
        price: {
            number: "Enter numbers only",
        },
        quantity: {
            number: "Enter numbers only",
        },
    }
});


var quill = new Quill('#productDescription', {
    theme: 'snow'
});

const wrapper = document.querySelector("#wrapper");
const fileName = document.querySelector("#file-name");
const defaultBtn = document.querySelector("#default-btn");
const customBtn = document.querySelector("#custom-btn");
const img = document.querySelector("#productImg");
let regExp = /[0-9a-zA-Z\^\&\'\@\{\}\[\]\,\$\=\!\-\#\(\)\.\%\+\~\_ ]+$/;

function defaultBtnActive() {
    defaultBtn.click();
}

defaultBtn.addEventListener("change", function () {
    const file = this.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function () {
            const result = reader.result;
            img.src = result;
            wrapper.classList.add("active");
        }
        reader.readAsDataURL(file);
    }
    if (this.value) {
        let valueStore = this.value.match(regExp);
        fileName.textContent = valueStore;
    }
});