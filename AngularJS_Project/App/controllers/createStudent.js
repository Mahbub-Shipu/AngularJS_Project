angular.module("projectApp")
    .controller("createStudentCtrl", ($scope, $routeParams, $location, apiUrl, ApiService) => {

        $scope.newStudent = null;
        $scope.newStudentMsg = "";

        $scope.insertStudent = (frm) => {
            ApiService.post(apiUrl + "Products", $scope.newStudent, null)
                .then(r => {
                    console.log(r);
                    $scope.$emit('studentInserted', r.data);
                    $scope.newStudent = {};
                    // $location.path("/companies");
                    $scope.newStudentMsg = "Data inserted";
                    frm.$setPristine();
                }, err => {
                    console.log(err);
                });
        }
    });