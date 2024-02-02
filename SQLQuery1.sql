create procedure spGetWendorById
(
@id int
)
as
begin
 select * from Wendors where Id=@id
end
go