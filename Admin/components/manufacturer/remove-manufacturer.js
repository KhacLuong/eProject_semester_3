var url = 'https://localhost:7000/api/AdminManufacturer';

function removeManufacturer(removeId) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            var deleteUrl = url + "/" + removeId;
            axios.delete(deleteUrl)
                .then(response => {
                    console.log(response);
                })
                .then(() => {
                    var manufacturer = document.getElementById(`${removeId}`);
                    console.log(manufacturer);
                    manufacturer.parentNode.removeChild(manufacturer);
                });
        }
    })
}
