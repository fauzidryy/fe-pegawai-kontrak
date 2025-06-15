document.addEventListener('DOMContentLoaded', function () {
    const successMessage = document.body.dataset.success;
    const errorMessage = document.body.dataset.error;

    if (successMessage) {
        Swal.fire({
            title: 'Berhasil!',
            text: successMessage,
            icon: 'success',
            timer: 2000,
            showConfirmButton: false
        });
    }

    if (errorMessage) {
        Swal.fire({
            title: 'Gagal!',
            text: errorMessage,
            icon: 'error',
            timer: 2500,
            showConfirmButton: false
        });
    }
});
