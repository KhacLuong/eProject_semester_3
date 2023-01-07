var url = 'https://localhost:7000/api/AdminProduct?pageSize=100';

axios.get(url)
    .then(response => {
        if (response.status === 200) {
            document.querySelector('#list-products').innerHTML = '';
            var products = response.data.data.products;
            console.log(response);
            var content = ``;
            var check = ``;

            products.map(product => {
                if (product.status === "Active") {
                    check = `
                    <td>
                        <div class="d-flex align-items-center">
                            <div class="badge badge-success badge-dot m-r-10"></div>
                            <div>Active</div>
                        </div>
                    </td>`
                } else {
                    check = `
                    <td>
                        <div class="d-flex align-items-center">
                            <div class="badge badge-danger badge-dot m-r-10"></div>
                            <div>Inactive</div>
                        </div>
                    </td>`
                };

                content += `
                <tr id="${product.id}">
                    <td>${product.id}</td>
                    <td class="productCode">${product.code}</td>
                    <td class="productName">
                        <div class="d-flex align-items-center">
                            <img class="img-fluid rounded" src="${product.imageProductPath}" style="max-width: 60px" alt="">
                            <h6 class="m-b-0 m-l-10">${product.name}</h6>
                        </div>
                    </td>
                    <td class="productCategory">${product.category}</td>
                    <td class="productManufacturer">${product.manufacturer}</td>
                    <td class="productAuthor">${product.author}</td>
                    <td class="productPrice">${product.price} $</td>
                    <td class="productQuantity">${product.quantity}</td>
                    <td>${product.description}</td>`

                    + check +

                    `
                    <td class="text-right">
                        <a href="product-update.html?id=${product.id}" class="btn btn-icon btn-hover btn-sm btn-rounded pull-right">
                            <i class="anticon anticon-edit"></i>
                        </a>
                        <button class="btn btn-icon btn-hover btn-sm btn-rounded" onclick="removeProduct(${product.id})">
                            <i class="anticon anticon-delete"></i>
                        </button>
                        <button class="btn btn-icon btn-hover btn-sm btn-rounded" onclick="window.location='product-detail.html?id=${product.id}'">
                            <i class="anticon anticon-file-text"></i>
                        </button>
                    </td>
                </tr>
                `
                    ;
            });
            document.querySelector('#list-products').innerHTML = content;


        }
    })


