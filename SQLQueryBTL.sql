create table tblLop
( 
   maLopTC_PK nvarchar(30) not null ,
   maSV_PK nvarchar(30) not null,
   diemChuyenCan float not null,
  diemGiuaKy float not null,
  diemThi float not null,
  diemTBC float not null,
  constraint khoa_chinh_TBLop primary key (maLopTC_PK,maSV_PK)
)

CREATE TABLE [dbo].[GiangVien](
	[maGV] [nvarchar](30) NOT NULL,
	[tenGV] [nvarchar](30) NOT NULL,
	[ngaySinh] [datetime] NOT NULL,
	[gioiTinh] [nvarchar](5) NOT NULL,
	[diaChi] [nvarchar](50) NOT NULL,
	[soDienThoai] [nchar](14) NOT NULL,
	[email] [nchar](50) NOT NULL,
	[hocHam] [nvarchar](30) NULL,
	[hocVi] [nvarchar](30) NULL,
	[maKhoaFK] [nvarchar](30) not NULL,
 CONSTRAINT [PK_GiangVien] PRIMARY key ([maGV])
 )
GO


select * from SinhVien

select * from Khoa

select * from LopHanhChinh

delete from SinhVien where maSV=N'sv02'

select * from SinhVien where maLopHC_FK=N'1810A01'

--tìm sinh viên theo khoa
select maSV,tenSV,ngaySinh,gioiTinh,diaChi,SinhVien.soDienThoai,email,namNhapHoc,maLopHC_FK,tenKhoa as N'maKhoaFK'
from SinhVien left join Khoa on SinhVien.maKhoaFK = Khoa.maKhoa
                                where SinhVien.maKhoaFK=N'cntt'

--tìm sinh viên theo lớp và khoa
select maSV,tenSV,ngaySinh,gioiTinh,diaChi,SinhVien.soDienThoai,email,namNhapHoc,maLopHC_FK,tenKhoa as N'maKhoaFK'
from SinhVien left join LopHanhChinh on SinhVien.maLopHC_FK = LopHanhChinh.maLopHC 
				left join Khoa on SinhVien.maKhoaFK = Khoa.maKhoa
where SinhVien.maLopHC_FK=N'17a1' and SinhVien.maKhoaFK=N'cntt'


--tìm sv theo tên + lớp + khoa
select maSV,tenSV,ngaySinh,gioiTinh,diaChi,SinhVien.soDienThoai,email,namNhapHoc,maLopHC_FK,tenKhoa as N'maKhoaFK'
from SinhVien left join LopHanhChinh on SinhVien.maLopHC_FK = LopHanhChinh.maLopHC 
				left join Khoa on SinhVien.maKhoaFK = Khoa.maKhoa
where SinhVien.maLopHC_FK=N'17a1' and SinhVien.maKhoaFK=N'cntt' and SinhVien.maSV=N'dsdsd'

select maSV,tenSV,ngaySinh,gioiTinh,diaChi,SinhVien.soDienThoai,email,namNhapHoc,maLopHC_FK,tenKhoa as N'maKhoaFK'
from SinhVien left join LopHanhChinh on SinhVien.maLopHC_FK = LopHanhChinh.maLopHC 
				left join Khoa on SinhVien.maKhoaFK = Khoa.maKhoa
where SinhVien.maLopHC_FK=N'17a1' and SinhVien.maKhoaFK=N'cnsh' and SinhVien.tenSV like N'%v%'

---tìm lớp tc theo mã khoa
select maLopTC,MonHoc.tenMon as N'maMonFK',GiangVien.tenGV as N'maGiangVienFK',hocKy,namHoc,tenKhoa as N'maKhoaFK'
                                        from LopTinChi left join Khoa on LopTinChi.maKhoaFK = Khoa.maKhoa
                                                        left join MonHoc on LopTinChi.maMonFK = MonHoc.maMon
                                                        left join GiangVien on LopTinChi.maLopTC = GiangVien.maGV
                                    where LopTinChi.maKhoaFK=N'0003'
---tìm lớp tc theo mã lớp
select maLopTC,MonHoc.tenMon as N'maMonFK',GiangVien.tenGV as N'maGiangVienFK',hocKy,namHoc,tenKhoa as N'maKhoaFK'
                                        from LopTinChi left join Khoa on LopTinChi.maKhoaFK = Khoa.maKhoa
                                                        left join MonHoc on LopTinChi.maMonFK = MonHoc.maMon
                                                        left join GiangVien on LopTinChi.maLopTC = GiangVien.maGV
                                    where LopTinChi.maLopTC=N'0003'

---tìm lớp tc theo năm học
select maLopTC,MonHoc.tenMon as N'maMonFK',GiangVien.tenGV as N'maGiangVienFK',hocKy,namHoc,tenKhoa as N'maKhoaFK'
                                        from LopTinChi left join Khoa on LopTinChi.maKhoaFK = Khoa.maKhoa
                                                        left join MonHoc on LopTinChi.maMonFK = MonHoc.maMon
                                                        left join GiangVien on LopTinChi.maLopTC = GiangVien.maGV
                                    where LopTinChi.namHoc like N'2019%-%2020%' and LopTinChi.maKhoaFK=N'0003'

----
select maLopTC_PK,maSV_PK,tenSV,ngaySinh,diemChuyenCan,diemGiuaKy,diemThi,diemTBC 
from BangDiem left join SinhVien on BangDiem.maSV_PK=SinhVien.maSV
where maLopTC_PK=N'0019'

----

create trigger tinhDiemTBC
on BangDiem
for insert,update
as 
  begin
	declare @maLopTc nvarchar(30),@maSV nvarchar(30), @diemcc float,@diemgk float,@diemthi float
	select @maLopTc=maLopTC_PK,@maSV=maSV_PK,@diemcc=diemChuyenCan,@diemgk=diemGiuaKy,@diemthi=diemThi from inserted
	declare @diemtb float=(@diemcc*10+@diemgk*20+@diemthi*70)/100
	update BangDiem
	set diemTBC=@diemtb 
	where maLopTC_PK=@maLopTc and maSV_PK=@maSV
  end
go

/* Liệt kê danh sách sinh viên gồm các thông tin sau: Họ và tên sinh viên, Giới tính, Tuổi, Mã khoa. Trong
đó Giới tính hiển thị ở dạng Nam/Nữ tuỳ theo giá trị của field Phai là Yes hay No, Tuổi sẽ được tính bằng cách lấy năm hiện hành trừ cho năm sinh.
 Danh sách sẽ được sắp xếp theo thứ tự Tuổi giảm dần
select hotensv=hosv+' '+tensv,gioitinh=case when phai=1 then N'Yes' else  N'No' end,tuoi=year(getdate())-year(ngaysinh)+1,khoa.makhoa
from khoa inner join sinhvien on khoa.makhoa=sinhvien.makhoa
order by tuoi desc*/ 

--lấy danh sách sv theo lớp hành chính
create proc dsSVlopHC(@malop nvarchar(30))
as
begin
	select maSV,tenSV,ngaySinh,gioiTinh,diaChi,soDienThoai,email,namNhapHoc,maLopHC_FK
	from SinhVien
	where maLopHC_FK=@malop
end

select maSV,tenSV,sv.ngaySinh,sv.gioiTinh,sv.diaChi,sv.soDienThoai,sv.email,namNhapHoc,maLopHC_FK,gv.tenGV,siso=count(maSV)
from SinhVien sv,GiangVien gv,LopHanhChinh lhc
where maLopHC=N'1810A01' and sv.maLopHC_FK=lhc.maLopHC and lhc.maGVChuNhiemFK=gv.maGV
group by maSV,tenSV,sv.ngaySinh,sv.gioiTinh,sv.diaChi,sv.soDienThoai,sv.email,namNhapHoc,maLopHC_FK,gv.tenGV

select maSV,siso=count(maSV),tenSV,sv.ngaySinh,sv.gioiTinh,sv.diaChi,sv.soDienThoai,sv.email,namNhapHoc,maLopHC_FK,gv.tenGV
from SinhVien sv inner join LopHanhChinh lhc 
		on sv.maLopHC_FK=lhc.maLopHC inner join GiangVien gv 
		on lhc.maGVChuNhiemFK=gv.maGV
where sv.maLopHC_FK=N'1810A01'
group by maSV,tenSV,sv.ngaySinh,sv.gioiTinh,sv.diaChi,sv.soDienThoai,sv.email,namNhapHoc,maLopHC_FK,gv.tenGV

select maSV,tenSV,sv.ngaySinh,sv.gioiTinh,sv.diaChi,sv.soDienThoai,sv.email,namNhapHoc,maLopHC_FK,siso=count(*)
from SinhVien sv
where maLopHC_FK=N'1810A01'
group by maSV
--drop proc dsSVlopHC 
exec dsSVlopHC N'1810A01'
--lấy danh sách sv theo lớp tín chỉ
create proc dsSVlopTC(@maloptc nvarchar(30))
as
begin
	select maSV,tenSV,ngaySinh,gioiTinh,maLopHC_FK,diemChuyenCan,diemGiuaKy,diemThi,diemTBC,maLopTC_PK,tenMon,soTinChi
	from SinhVien,BangDiem,LopTinChi,MonHoc
	where maLopTC_PK=@maloptc and SinhVien.maSV=BangDiem.maSV_PK and LopTinChi.maLopTC=BangDiem.maLopTC_PK and MonHoc.maMon=LopTinChi.maMonFK
end

exec dsSVlopTC N'0019'

--lấy danh sách các lớp tín chỉ theo học kỳ

select maLopTC,hocKy,namHoc,maGiangVienFK,tenGV,tenMon,soTinChi
from LopTinChi,MonHoc,GiangVien
where LopTinChi.maMonFK=MonHoc.maMon and LopTinChi.maGiangVienFK=GiangVien.maGV and hocKy=1 and namHoc like N'2019%-%2020' and LopTinChi.maKhoaFK=N'0002'

create proc dsLopTCHocKy(@hocky int,@namhoc nvarchar(15),@makhoa nvarchar(30))
as
begin
select maLopTC,hocKy,namHoc,maGiangVienFK,tenGV,tenMon,soTinChi
from LopTinChi inner join MonHoc on  LopTinChi.maMonFK=MonHoc.maMon 
				inner join GiangVien on LopTinChi.maGiangVienFK=GiangVien.maGV
where hocKy=@hocky and namHoc like @namhoc and LopTinChi.maKhoaFK=@makhoa
end

exec dsLopTCHocKy 1,N'2019%-%2020',N'0002'


select *
from LopTinChi
----lấy danh sách điểm sv theo lớp hành chính và theo học kỳ  --chưa viết được query

select maSV,tenSV,ngaySinh,gioiTinh,maLopHC_FK--,diemChuyenCan,diemGiuaKy,diemThi,diemTBC,maLopTC_PK
from SinhVien inner join BangDiem on SinhVien.maSV=BangDiem.maSV_PK			
where  SinhVien.maLopHC_FK=N'1810A01' --SinhVien.maSV=N'18A10001' and
union All
select *-- MonHoc.soTinChi,BangDiem.diemTBC
from LopTinChi inner join BangDiem on LopTinChi.maLopTC=BangDiem.maLopTC_PK
				inner join MonHoc on MonHoc.maMon=LopTinChi.maMonFK
where BangDiem.maSV_PK=N'18A10001'
 


create proc dsDiemlopHC(@malophc nvarchar(30),@hocky int,@namhoc nvarchar(15),@maloptc nvarchar(30))--,
as
begin
	select maSV,tenSV,ngaySinh,gioiTinh,SinhVien.maLopHC_FK,soTinChi=sum(MonHoc.soTinChi),diemtb=sum(BangDiem.diemTBC*MonHoc.soTinChi)/sum(MonHoc.soTinChi)--,soTCNo=case when BangDiem.diemTBC<4.0 then sum(MonHoc.soTinChi) end
	from SinhVien,LopTinChi,BangDiem,MonHoc,LopHanhChinh
	where SinhVien.maSV=BangDiem.maSV_PK and LopTinChi.maLopTC=BangDiem.maLopTC_PK and MonHoc.maMon=LopTinChi.maMonFK and SinhVien.maLopHC_FK=LopHanhChinh.maLopHC and SinhVien.maLopHC_FK=N'1810A01' and LopTinChi.hocKy=1 and LopTinChi.namHoc like N'2019%-%2020'-- and LopTinChi.maKhoaFK=N'Luật'
	group by SinhVien.maSV,SinhVien.tenSV,SinhVien.ngaySinh,SinhVien.gioiTinh,SinhVien.maLopHC_FK
end

/*
select maSV,tenSV,ngaySinh,gioiTinh,LopHanhChinh.maLopHC,soTinChi=sum(MonHoc.soTinChi),diemtb=sum(BangDiem.diemTBC*MonHoc.soTinChi)/sum(MonHoc.soTinChi) --,soTCNo=case when BangDiem.diemTBC<4.0 then sum(MonHoc.soTinChi) end
from LopHanhChinh lhc inner join SinhVien sv
	on lhc.maLopHC=sv.maLopHC_FK inner join BangDiem bd 
	on sv.maSV=bd.maSV_PK inner join LopTinChi ltc
	on bd.maLopTC_PK=ltc.maLopTC inner join MonHoc mh
	on ltc.maMonFK=mh.maMon
where lhc.maLopHC=N'1810A01'
group by SinhVien.maSV,SinhVien.tenSV,SinhVien.ngaySinh,SinhVien.gioiTinh,maLopHC*/