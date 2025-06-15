document.addEventListener("DOMContentLoaded", function () {
    const startInput = document.getElementById("startDate");
    const endInput = document.getElementById("endDate");

    endInput.addEventListener("change", function () {
        const startDate = new Date(startInput.value);
        const endDate = new Date(endInput.value);

        if (endInput.value && startInput.value && endDate < startDate) {
            Swal.fire({
                icon: 'error',
                title: 'Tanggal tidak valid',
                text: 'Tanggal habis kontrak tidak boleh lebih awal dari tanggal mulai kontrak!',
            });
            endInput.value = "";
        }
    });
});