angular.module("projectApp")
    .controller("editStudent", ($scope, $routeParams, $location, apiUrl, ApiService) => {

        var id = $routeParams.id;
        $scope.currentStudent = null;

        $scope.updateStudentMsg = "";
        if (id == null) {
            $scope.errorMsg = "Teacher id not available.";
            $location.path("/departments");
        }
        ApiService.get(apiUrl + "Students/" + id, null)
            .then(r => {
                $scope.currentStudent = r.data;
                console.log(r.data);
                //$scope.departmentToEdit.Eshtablished = $scope.departmentToEdit.Eshtablished.substring(0, 10);
            }, err => {
                console.log(err);
            });
        $scope.updateStudent = (frm) => {
            ApiService.put(apiUrl + "Students/" + id, $scope.currentStudent, null)
                .then(r => {
                    console.log(r);
                    $scope.$emit('studentUpdated', $scope.currentStudent);
                    $scope.updateStudentMsg = "Data updated."
                    // $location.path("/companies");
                }, err => {
                    console.log(err);
                });

        }

    });