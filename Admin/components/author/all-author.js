var url = 'https://localhost:7000/api/AdminAuthor';

axios.get(url)
    .then(response => {
        if (response.status === 200) {
            document.querySelector('#list-authors').innerHTML = '';
            var authors = response.data.data.authors;
            var content = ``;

            authors.map(author => {
                content += `
                <tr id="${author.id}">
                    <td>${author.id}</td>
                    <td class="authorName">
                        <h6 class="m-b-0 m-l-10">${author.name}</h6>
                    </td>
                    <td class="authorEmail">${author.email}</td>
                    <td class="authorPhone">${author.phone}</td>
                    <td class="text-right">
                        <a href="author-update.html?id=${author.id}" class="btn btn-icon btn-hover btn-sm btn-rounded pull-right">
                            <i class="anticon anticon-edit"></i>
                        </a>
                        <button class="btn btn-icon btn-hover btn-sm btn-rounded" onclick="removeAuthor(${author.id})">
                            <i class="anticon anticon-delete"></i>
                        </button>
                        <button class="btn btn-icon btn-hover btn-sm btn-rounded"  onclick="window.location='author-detail.html?id=${author.id}'">
                            <i class="anticon anticon-file-text"></i>
                        </button>
                    </td>
                </tr>
            `;
            });
            document.querySelector('#list-authors').innerHTML = content;

            const searchInput = document.getElementById('search');
            const rows = document.querySelectorAll('#list-authors tr');

            searchInput.addEventListener('keyup', function (event) {
                const name = event.target.value.toLowerCase();
                const email = event.target.value.toLowerCase();
                const phoneNumber = event.target.value;

                rows.forEach((row) => {
                    if ((row.querySelector('.authorName').textContent.trim().toLowerCase().startsWith(name)) || 
                        (row.querySelector('.authorEmail').textContent.trim().toLowerCase().startsWith(email)) || 
                        (row.querySelector('.authorPhone').textContent.startsWith(phoneNumber))) {
                        row.style.display = "table-row";
                    } else {
                        row.style.display = 'none';
                    }
                })
            })
        }
    });




