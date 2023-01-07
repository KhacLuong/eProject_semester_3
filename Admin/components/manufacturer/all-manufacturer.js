var url = 'https://localhost:7000/api/AdminManufacturer';

axios.get(url)
    .then(response => {
        if (response.status === 200) {
            document.querySelector('#list-manufacturers').innerHTML = '';
            var manufacturers = response.data.data.manufacturers;
            var content = ``;

            manufacturers.map(manufacturer => {
                content += `
                <tr id="${manufacturer.id}">
                    <td>${manufacturer.id}</td>
                    <td class="manufacturerCode">${manufacturer.code}</td>
                    <td class="manufacturerName">
                        <h6 class="m-b-0 m-l-10">${manufacturer.name}</h6>
                    </td>
                    <td class="manufacturerEmail">${manufacturer.email}</td>
                    <td class="manufacturerPhone">${manufacturer.phoneNumber}</td>
                    <td class="manufacturerAddress">${manufacturer.address}</td>
                    <td class="text-right">
                        <a href="manufacturer-update.html?id=${manufacturer.id}" class="btn btn-icon btn-hover btn-sm btn-rounded pull-right">
                            <i class="anticon anticon-edit"></i>
                        </a>
                        <button class="btn btn-icon btn-hover btn-sm btn-rounded" onclick="removeManufacturer(${manufacturer.id})">
                            <i class="anticon anticon-delete"></i>
                        </button>
                        <button class="btn btn-icon btn-hover btn-sm btn-rounded" onclick="window.location='manufacturer-detail.html?id=${manufacturer.id}'">
                            <i class="anticon anticon-file-text"></i>
                        </button>
                    </td>
                </tr>
            `;
            });
            document.querySelector('#list-manufacturers').innerHTML = content;

            const searchInput = document.getElementById('search');
            const rows = document.querySelectorAll('#list-manufacturers tr');

            searchInput.addEventListener('keyup', function (event) {
                const code = event.target.value.toLowerCase();
                const name = event.target.value.toLowerCase();
                const email = event.target.value.toLowerCase();
                const phoneNumber = event.target.value;
                const address = event.target.value.toLowerCase();

                rows.forEach((row) => {
                    if ((row.querySelector('.manufacturerCode').textContent.trim().toLowerCase().startsWith(code)) ||
                        (row.querySelector('.manufacturerName').textContent.trim().toLowerCase().startsWith(name)) ||
                        (row.querySelector('.manufacturerEmail').textContent.trim().toLowerCase().startsWith(email)) ||
                        (row.querySelector('.manufacturerPhone').textContent.startsWith(phoneNumber)) ||
                        (row.querySelector('.manufacturerAddress').textContent.startsWith(address))) {

                        row.style.display = "table-row";
                    } else {
                        row.style.display = 'none';
                    }
                })

            })
        }
    });

