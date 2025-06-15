namespace FE_PegawaiKontrak.Models
{
    public class Pegawai
    {
        public int Id { get; set; }
        public string KodePegawai { get; set; } = string.Empty;
        public required string NamaPegawai { get; set; }
        public int CabangId { get; set; }
        public int JabatanId { get; set; }
        public DateTime TanggalMulaiKontrak { get; set; }
        public DateTime TanggalHabisKontrak { get; set; }

        public string? NamaCabang { get; set; }
        public string? NamaJabatan { get; set; }
    }
}
