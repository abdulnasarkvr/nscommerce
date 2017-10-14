
angular.module('controllers', [])

     .controller('ProductController', ['$scope', '$window', '$http', '$location', 'AuthService', 'Upload', 'toaster',
         function ($scope, $window,$http,$location, AuthService, Upload, toaster) {                  

         $scope.SaveProduct = function (isRedirect) {

             if ($scope.newProductForm.$valid) {

                 $scope.isloading = true;

                 var request = {
                     method: 'post'
                     , url: '/API/Product/Create'
                     , data: {
                         ProductCode: $scope.product.productCode
                         , ProductName: $scope.product.productName
                     }
                 };

                 $http(request).then(

                     function successCallback(response) {
                       
                     $scope.isloading = false;
                    
                     if (response.data.status == 0) {

                         toaster.pop('success', "Success", "Saved");

                         $scope.productId = response.data.returnData.id;

                         if (isRedirect) {
                          
                             $location.path('product/update/' + $scope.productId);
                         }
                     }
                     else {
                         toaster.pop('error', "Error", "Not saved");
                     }               
                     }

                   , function errorCallback(response) {
                       
                     $scope.isloading = false;

                     toaster.pop('error', "Error", response);

                 });                  
                
             }
         };

         

        
             // upload later on form submit or something similar
         $scope.submit = function () {
            
            // if ($scope.form.file.$valid && $scope.file) {
                
                 //$scope.upload($scope.file);
                
             //}

             $scope.uploadFiles($scope.files)
         };

             // upload on file select or drop
         $scope.upload = function (file) {
             alert('Filu');
             Upload.upload({
                 url: '/API/Files/Upload',
                 method: "POST",
                 data: {file: file}
             }).then(function (resp) {
                 console.log('Success ' + resp.config.data.file.name + 'uploaded. Response: ' + resp.data);
             }, function (resp) {
                 console.log('Error status: ' + resp.status);
             }, function (evt) {
                 var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                 console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
             });
         };
             // for multiple files:
         $scope.uploadFiles = function (files) {
            if (files && files.length) {
                 for (var i = 0; i < files.length; i++) {
                     alert(i);
                     // Upload.upload(files[i]);
                     $scope.upload(files[i]);
                  
             }
             // or send them all together for HTML5 browsers:
          //Upload.upload({..., data: {file: files}, ...})...;
         }
     }

     }]);