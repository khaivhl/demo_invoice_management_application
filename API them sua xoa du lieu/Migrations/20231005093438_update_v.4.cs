using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_them_sua_xoa_du_lieu.Migrations
{
    /// <inheritdoc />
    public partial class update_v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    KhachHangId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.KhachHangId);
                });

            migrationBuilder.CreateTable(
                name: "Loaisanpham",
                columns: table => new
                {
                    LoaiSanPhamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loaisanpham", x => x.LoaiSanPhamId);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    HoaDonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhachHangId = table.Column<int>(type: "int", nullable: false),
                    TenHangHoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaGiaoDich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongTien = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.HoaDonId);
                    table.ForeignKey(
                        name: "FK_HoaDon_KhachHang_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "KhachHang",
                        principalColumn: "KhachHangId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    SanPhamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiSanPhamId = table.Column<int>(type: "int", nullable: false),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaThanh = table.Column<double>(type: "float", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KyHieuSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.SanPhamId);
                    table.ForeignKey(
                        name: "FK_SanPham_Loaisanpham_LoaiSanPhamId",
                        column: x => x.LoaiSanPhamId,
                        principalTable: "Loaisanpham",
                        principalColumn: "LoaiSanPhamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDon",
                columns: table => new
                {
                    ChiTietHoaDonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoaDonId = table.Column<int>(type: "int", nullable: false),
                    SanPhamId = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DVT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoaDon", x => x.ChiTietHoaDonId);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDon_HoaDon_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "HoaDon",
                        principalColumn: "HoaDonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDon_SanPham_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "SanPham",
                        principalColumn: "SanPhamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_HoaDonId",
                table: "ChiTietHoaDon",
                column: "HoaDonId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_SanPhamId",
                table: "ChiTietHoaDon",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_KhachHangId",
                table: "HoaDon",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_LoaiSanPhamId",
                table: "SanPham",
                column: "LoaiSanPhamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietHoaDon");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "Loaisanpham");
        }
    }
}
