angular.module("projectApp")
    .controller("teacherController", ($scope, $routeParams, $location, apiUrl, ApiService) => {
        var id = $routeParams.id;
        $scope.currentTeacher = null;
        $scope.newTeacher = null;
        $scope.updateTeacherMsg = "";
        if (id == null) {
            $scope.errorMsg = "Teacher id not available.";
            $location.path("/departments");
        }
        ApiService.get(apiUrl + "Teachers/" + id, null)
            .then(r => {
                $scope.currentTeacher = r.data;
                console.log(r.data);
            }, err => {
                console.log(err);
            });
        $scope.updateTeacher = (frm) => {
            ApiService.put(apiUrl + "Teachers/" + id, $scope.currentTeacher, null)
                .then(r => {
                    console.log(r);
                    $scope.$emit('teacherUpdated', $scope.currentTeacher);
                    $scope.updateTeacherMsg = "Data updated."
                    // $location.path("/companies");
                }, err => {
                    console.log(err);
                });

        }
        $scope.insertTeacher = () => {
            ApiService.post(apiUrl + "Teachers/", $scope.newTeacher, null)
                .then(r => {
                    console.log(r);
                    $scope.$emit('teacherInserted', r.data);

                    // $location.path("/companies");
                }, err => {
                    console.log(err);
                });
        }
    });