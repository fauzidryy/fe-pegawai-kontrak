document.querySelectorAll('.btn-delete').forEach(btn => {
    btn.addEventListener('click', function () {
        const id = this.dataset.id;
        const nama = this.dataset.nama;

        Swal.fire({
            title: 'Yakin ingin menghapus?',
            text: `Cabang: ${nama}`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Ya, hapus!',
            cancelButtonText: 'Batal'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(`/Cabang/Delete/${id}`, {
                    method: 'POST'
                })
                    .then(response => {
                        if (response.ok) {
                            Swal.fire({
                                title: 'Berhasil!',
                                text: 'Cabang berhasil dihapus.',
                                icon: 'success',
                                timer: 2000,
                                showConfirmButton: false
                            }).then(() => {
                                location.reload();
                            });
                        } else {
                            Swal.fire(
                                'Gagal!',
                                'Terjadi kesalahan saat menghapus.',
                                'error'
                            );
                        }
                    });
            }
        });
    });
});
