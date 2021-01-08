angular.module("projectApp")
    .controller("createTeacherCtrl", ($scope, $routeParams, $location, apiUrl, ApiService) => {

        $scope.newTeacher = null;

        $scope.newTeacherMsg = "";
        $scope.insertTeacher = (frm) => {
            ApiService.post(apiUrl + "Teacher", $scope.newTeacher, null)
                .then(r => {
                    console.log(r);
                    $scope.$emit('teacherInserted', r.data);
                    $scope.newTeacherMsg = "Data inserted";
                    // $location.path("/companies");
                    frm.$setPristine();
                }, err => {
                    console.log(err);
                });
        }
    });