
create procedure PromoteStudents @Studies nvarchar(100), @Semester int
as
begin


	declare @IdStudies int = (select IdStudy from Studies where name=@Studies);
	if @IdStudies is null
	begin
		RAISERROR (15600,-1,-1, 'nie ma takich studiów');  
	end

	declare @IdEnrollment int = (select IdEnrollment from Enrollment where Semester=(@Semester+1) and IdStudy=@IdStudies);
	if @IdEnrollment is null
	begin
	declare @newIdEnrollment int = (Select max(IdEnrollment) from Enrollment)+1;
	INSERT INTO Enrollment VALUES (@newIdEnrollment, (@Semester+1), @IdStudies,  '1-MAR-2020');
	end

	update Student 
	set IdEnrollment = 
	(Select max(IdEnrollment) from Enrollment 
	inner join Studies on 
	Enrollment.IdStudy = Studies.IdStudy 
	where 
	Enrollment.Semester=(@Semester+1) and Studies.Name=@Studies) 
	where 
	IdEnrollment = (select IdEnrollment from Enrollment 
	inner join Studies on 
	Enrollment.IdStudy = Studies.IdStudy 
	where Enrollment.Semester=@Semester and Studies.Name=@Studies);


end
go