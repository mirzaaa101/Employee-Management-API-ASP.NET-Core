SELECT CONCAT(EBI.FirstName," ", EBI.LastName) AS FirstName,
       D.DeptName AS Dept,
       PG.Salary AS Salary
FROM EmployeeBasicInfo EBI
LEFT JOIN Departments D
    ON EBI.DeptId = D.DeptId
LEFT JOIN PayGrade PG
    ON EBI.PayGrade = PG.GradeNo
WHERE EmpId = @EmpId