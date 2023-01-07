var url = 'https://localhost:7000/api/AdminCategory?status=0';

axios.get(url)
    .then(response => {
        if (response.status === 200) {
            document.querySelector('#list-categories').innerHTML = '';
            var categories = response.data.data;
            var content = ``;
            var check = ``;

            categories.map(category => {
                if (category.status === "Active") {
                    check = `
                    <td class="categoryStatus">
                        <div class="d-flex align-items-center">
                            <div class="badge badge-success badge-dot m-r-10"></div>
                            <div>Active</div>
                        </div>
                    </td>`
                } else {
                    check = `
                    <td class="categoryStatus">
                        <div class="d-flex align-items-center">
                            <div class="badge badge-danger badge-dot m-r-10"></div>
                            <div>Inactive</div>
                        </div>
                    </td>`
                };

                content += `
                <tr id="${category.id}">
                    <td>${category.id}</td>
                    <td class="categoryCode">${category.code}</td>
                    <td class="categoryName">
                        <h6 class="m-b-0 m-l-10">${category.name}</h6>
                    </td>
                    <td>${category.description}</td>`

                    + check +

                    `
                    <td class="text-right">
                        <a href="category-update.html?id=${category.id}" class="btn btn-icon btn-hover btn-sm btn-rounded pull-right" onclick="getCategoryInfo(${category.id})">
                            <i class="anticon anticon-edit"></i>
                        </a>
                        <button class="btn btn-icon btn-hover btn-sm btn-rounded" onclick="removeCategory(${category.id})">
                            <i class="anticon anticon-delete"></i>
                        </button>
                        <button class="btn btn-icon btn-hover btn-sm btn-rounded" onclick="window.location='category-detail.html?id=${category.id}'">
                            <i class="anticon anticon-file-text"></i>
                        </button>
                    </td>
                </tr>
                `
                    ;
            });
            document.querySelector('#list-categories').innerHTML = content;

            const searchInput = document.getElementById('search');
            const rows = document.querySelectorAll('#list-categories tr');

            searchInput.addEventListener('keyup', function (event) {
                const code = event.target.value.toLowerCase();
                const name = event.target.value.toLowerCase();
                const status = event.target.value.toLowerCase();

                const phoneNumber = event.target.value;

                rows.forEach((row) => {
                    console.log(row.querySelector('.categoryCode').textContent.trim());
                    if ((row.querySelector('.categoryName').textContent.trim().toLowerCase().startsWith(name)) ||
                        (row.querySelector('.categoryStatus').textContent.trim().toLowerCase().startsWith(code)) ||
                        (row.querySelector('.categoryCode').textContent.trim().toLowerCase().startsWith(status))) {
                        row.style.display = "table-row";
                    } else {
                        row.style.display = 'none';
                    }
                })
            })
        }
    })


