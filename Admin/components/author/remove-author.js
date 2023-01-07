var url = 'https://localhost:7000/api/AdminAuthor';

function removeAuthor(removeId) {
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
                    var author = document.getElementById(`${removeId}`);
                    console.log(author);
                    author.parentNode.removeChild(author);
                });
        }
    })
}
