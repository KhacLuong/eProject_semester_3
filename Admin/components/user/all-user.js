const accessToken = JSON.parse(window.localStorage.getItem('accessToken'));

const instance = axios.create({
    baseURL: 'https://localhost:7000/api/',
})
const config = {
    headers: { Authorization: `Bearer ${accessToken}` }
};
instance.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`;

instance.get('AdminUser')
    .then(response => {
        if (response.status === 200) {
            document.querySelector('#list-users').innerHTML = '';
            var users = response.data.data.data;
            var content = ``;

            users.map(user => {
                console.log(user.userInfo);
                content += `
                <tr id="${user.id}">
                    <td>${user.id}</td>
                    <td class="userName">
                        <div class="d-flex align-items-center">
                            <img class="img-fluid rounded" src="${user.avatar}" style="max-width: 60px" alt="">
                            <h6 class="m-b-0 m-l-10">${user.name}</h6>
                        </div>
                    </td>
                    <td class="userEmail">${user.email}</td>
                    <td class="userPhone">${user.phone}</td>
                    <td class="userAdderss">${user.userInfo.address}</td>
                    <td class="userGender">${user.userInfo.gender}</td>
                    <td class="userRole">${user.userType}</td>
                    <td class="text-right">
                        <a href="user-update.html?id=${user.id}" class="btn btn-icon btn-hover btn-sm btn-rounded pull-right">
                            <i class="anticon anticon-edit"></i>
                        </a>
                        <button class="btn btn-icon btn-hover btn-sm btn-rounded" onclick="removeUser(${user.id})">
                            <i class="anticon anticon-delete"></i>
                        </button>
                        <button class="btn btn-icon btn-hover btn-sm btn-rounded"  onclick="window.location='user-detail.html?id=${user.id}'">
                            <i class="anticon anticon-file-text"></i>
                        </button>
                    </td>
                </tr>
            `;
            });
            document.querySelector('#list-users').innerHTML = content;

            /*
            const searchInput = document.getElementById('search');
            const rows = document.querySelectorAll('#list-users tr');

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
            */
        }
    });