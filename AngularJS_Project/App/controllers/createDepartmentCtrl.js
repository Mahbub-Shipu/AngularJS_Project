angular.module("projectApp")
    .controller("createDepartmentCtrl", ($scope, apiUrl, ApiService) => {
        /////////
        /// locals
        $scope.departmentInsertMsg = "";
        $scope.activeTab = 0;
        $scope.popup1 = {
            opened: false,
        };
        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };
        /////////////
        ///Model Related
        $scope.newDepartment = {};
        $scope.newStudent = {};
        $scope.students = [];
        $scope.newTeacher = {};
        $scope.teachers = [];
        $scope.departmentDone = () => {
            $scope.activeTab = 1;
        };
        $scope.studentDone = function (frm) {
            //console.log($scope.newProduct);

            $scope.students.push($scope.newStudent);

            $scope.newStudent = {};
            frm.$setPristine();

            console.log($scope.students);
        };
        $scope.studentDel = (index) => {
            $scope.students.splice(index, 1);
        };
        $scope.studentPre = () => {
            $scope.activeTab = 0;
        };
        $scope.studentNext = () => {
            $scope.activeTab = 2;
        };
        $scope.teacherPre = () => {
            $scope.activeTab = 1;
        };
        $scope.teacherNext = (frms) => {
            $scope.newDepartment.Students = $scope.students;
            $scope.newDepartment.Teachers = $scope.teachers;
            ApiService.post(apiUrl + "Departments", $scope.newDepartment, null)
                .then(r => {
                    $scope.departmentInsertMsg = "Data inserted successfully.";
                    $scope.$emit('departmentInserted', r.data);
                    $scope.newDepartment = {};
                    $scope.newStudent = {};
                    $scope.students = [];
                    $scope.newTeacher = {};
                    $scope.teachers = [];
                    $scope.activeTab = 0;
                    frms[0].$setPristine();
                    frms[1].$setPristine();
                    frms[2].$setPristine();
                    console.log(frms);
                }, err => {
                    console.log(err);
                });
        };
        $scope.teacherDel = (index) => {
            $scope.teachers.splice(index, 1)
        };
        
        $scope.teacherDone = function (frm) {
            //console.log($scope.newProduct);

            $scope.teachers.push($scope.newTeacher);

            $scope.newTeacher = {};
            frm.$setPristine();

            console.log($scope.teachers);
        };
    });