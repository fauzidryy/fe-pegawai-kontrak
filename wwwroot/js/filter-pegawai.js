document.querySelectorAll('.filter-btn').forEach(btn => {
    btn.addEventListener('click', () => {
        const filter = btn.getAttribute('data-filter');
        document.querySelectorAll('tbody tr').forEach(row => {
            const status = row.getAttribute('data-status');
            if (filter === 'all' || status === filter) {
                row.style.display = '';
            } else {
                row.style.display = 'none';
            }
        });

        document.querySelectorAll('.filter-btn').forEach(b => b.classList.remove('active'));
        btn.classList.add('active');
    });
});