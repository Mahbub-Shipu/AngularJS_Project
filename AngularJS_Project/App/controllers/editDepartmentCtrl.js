angular.module("projectApp")
    .controller("editDepartmentCtrl", ($scope, $routeParams, $uibModal, $log, $document, apiUrl, customUrl, ApiService) => {
        $scope.departmentEditMsg = "";

        var id = $routeParams.id;
        $scope.departmentToEdit = {};
        $scope.newDepartmentStudent = {};
        $scope.newDepartmentTeacher = {};
        ApiService.get(customUrl + "Departments/" + id)
            .then(r => {
                $scope.departmentToEdit = r.data;
                console.log(r.data)
                //$scope.departmentToEdit.Eshtablished = $scope.departmentToEdit.Eshtablished.substring(0, 10);
            }, err => {
                console.log(err);
            });
        $scope.editDone = (frm) => {
            ApiService.put(apiUrl + "Departments/" + $scope.departmentToEdit.CompanyId, $scope.departmentToEdit, null)
                .then(r => {
                    $scope.$emit("departmentUpdated", $scope.departmentToEdit);
                    $scope.departmentEditMsg = "Data updated successfuly."
                }, err => {
                    console.log(err);
                });
        }
        $scope.deleteDepartmentTeacher = (index) => {
            $scope.departmentToEdit.Teachers.splice(index, 1);

        }
        $scope.deleteDepartmentStudent = (index) => {
            $scope.departmentToEdit.Students.splice(index, 1);
        }
        $scope.openAddDepartmentStudentDialog = function () {
            console.log($scope.departmentToEdit.DepartmentId)
            $("#addDepartmentStudentModal").modal('show');
        }
        $scope.addStudentToDepartment = (frm) => {
            $scope.newDepartmentStudent.DepartmentId = $scope.departmentToEdit.DepartmentId;
            $scope.departmentToEdit.Products.push($scope.newDepartmentStudent);
            frm.$setPristine();
            $scope.newDepartmentStudent = {};
            $("#addDepartmentStudentModal").modal('hide');
        }
        $scope.openAddDepartmentTeacherDialog = function () {
            console.log($scope.departmentToEdit.DepartmentId)
            $("#addDepartmentTeacherModal").modal('show');
        }
        $scope.addTeacherToDepartment = (frm) => {
            $scope.newDepartmentTeacher.DepartmentId = $scope.departmentToEdit.DepartmentId;
            $scope.departmentToEdit.Teachers.push($scope.newDepartmentTeacher);
            frm.$setPristine();
            $scope.newDepartmentTeacher = {};
            $("#addDepartmentTeacherModal").modal('hide');
        }
    });