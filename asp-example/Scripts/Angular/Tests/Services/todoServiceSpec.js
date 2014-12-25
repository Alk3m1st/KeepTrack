'use strict';

describe('TodoService', function () {
    var $httpBackend, service, todos, archiveResponse, addTodoResponse;
	
    beforeEach(module('KeepTrack'));

    beforeEach(inject(function ($injector) {
        $httpBackend = $injector.get('$httpBackend');

        // Mock responses
        $httpBackend.when('POST', '/HomeJson/Delete').respond({ response: 'success' });
        archiveResponse = $httpBackend.when('POST', '/HomeJson/Archive');
        addTodoResponse = $httpBackend.when('POST', '/HomeJson/AddTodo');

        service = $injector.get('TodoService');

        todos = [
            {
                $$hashKey: '005',
                Archived: false,
                Completed: '',
                Created: 'Wednesday',
                Description: 'Description 1',
                ElapsedDaysClass: '',
                Id: '90685688-9b26-4ce1-82b6-b3652521604f'
            },
            {
                $$hashKey: '006',
                Archived: false,
                Completed: '',
                Created: 'Wednesday',
                Description: 'Description 2',
                ElapsedDaysClass: '',
                Id: 'b6d17f69-f40b-4d7e-bd1b-322cdf862efa'
            },
            {
                $$hashKey: '007',
                Archived: true,
                Completed: 'Wednesday',
                Created: 'Wednesday',
                Description: 'Description 3',
                ElapsedDaysClass: 'one-day-elapsed',
                Id: '0124e316-8ace-48d8-bd3a-3e7a2f1dba32'
            },
            {
                $$hashKey: '008',
                Archived: true,
                Completed: 'Wednesday',
                Created: 'Wednesday',
                Description: 'Description 4',
                ElapsedDaysClass: 'one-day-elapsed',
                Id: '675ac400-da4f-4630-9076-b4e64f98b41f'
            }
        ];
    }));

    afterEach(function () {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    it("Should delete the first non-archived item", function () {
        $httpBackend.expectPOST('/HomeJson/Delete');
        var item = todos[0];

        service.deleteTodo(item, todos);

        $httpBackend.flush();

        expect(todos.length).toBe(3);
        expect(todos[0].Id).toBe('b6d17f69-f40b-4d7e-bd1b-322cdf862efa');
        expect(todos[1].Id).toBe('0124e316-8ace-48d8-bd3a-3e7a2f1dba32');
        expect(todos[2].Id).toBe('675ac400-da4f-4630-9076-b4e64f98b41f');
    });

    it("Should delete the second non-archived item", function () {
        $httpBackend.expect('POST', '/HomeJson/Delete');
        var item = todos[1];

        service.deleteTodo(item, todos);

        $httpBackend.flush();

        expect(todos.length).toBe(3);
        expect(todos[0].Id).toBe('90685688-9b26-4ce1-82b6-b3652521604f');
        expect(todos[1].Id).toBe('0124e316-8ace-48d8-bd3a-3e7a2f1dba32');
        expect(todos[2].Id).toBe('675ac400-da4f-4630-9076-b4e64f98b41f');
    });

    it("Should delete the first archived item", function () {
        $httpBackend.expect('POST', '/HomeJson/Delete');
        var item = todos[2];

        service.deleteTodo(item, todos);

        $httpBackend.flush();

        expect(todos.length).toBe(3);
        expect(todos[0].Id).toBe('90685688-9b26-4ce1-82b6-b3652521604f');
        expect(todos[1].Id).toBe('b6d17f69-f40b-4d7e-bd1b-322cdf862efa');
        expect(todos[2].Id).toBe('675ac400-da4f-4630-9076-b4e64f98b41f');
    });

    it("Should delete the last (archived) item", function () {
        $httpBackend.expect('POST', '/HomeJson/Delete');
        var item = todos[3];

        service.deleteTodo(item, todos);

        $httpBackend.flush();

        expect(todos.length).toBe(3);
        expect(todos[0].Id).toBe('90685688-9b26-4ce1-82b6-b3652521604f');
        expect(todos[1].Id).toBe('b6d17f69-f40b-4d7e-bd1b-322cdf862efa');
        expect(todos[2].Id).toBe('0124e316-8ace-48d8-bd3a-3e7a2f1dba32');
    });

    it("Should archive the first non-archived item and move it to the top of the archived items", function () {
        $httpBackend.expect('POST', '/HomeJson/Archive');
        var item = todos[0];
        var updatedItem = JSON.parse(JSON.stringify(item)); // Clone item
        updatedItem.Archived = true;
        
        archiveResponse.respond(updatedItem);

        service.archiveTodo(item, todos);
        $httpBackend.flush();

        expect(todos.length).toBe(4);
        expect(todos[0].Id).toBe('b6d17f69-f40b-4d7e-bd1b-322cdf862efa');
        expect(todos[1].Id).toBe('90685688-9b26-4ce1-82b6-b3652521604f');
        expect(todos[2].Id).toBe('0124e316-8ace-48d8-bd3a-3e7a2f1dba32');
        expect(todos[3].Id).toBe('675ac400-da4f-4630-9076-b4e64f98b41f');
    });

    it("Should archive the second non-archived item and move it to the top of the archived items", function () {
        $httpBackend.expect('POST', '/HomeJson/Archive');
        var item = todos[1];
        var updatedItem = JSON.parse(JSON.stringify(item)); // Clone item
        updatedItem.Archived = true;

        archiveResponse.respond(updatedItem);

        service.archiveTodo(item, todos);
        $httpBackend.flush();

        expect(todos.length).toBe(4);
        expect(todos[0].Id).toBe('90685688-9b26-4ce1-82b6-b3652521604f');
        expect(todos[1].Id).toBe('b6d17f69-f40b-4d7e-bd1b-322cdf862efa');
        expect(todos[2].Id).toBe('0124e316-8ace-48d8-bd3a-3e7a2f1dba32');
        expect(todos[3].Id).toBe('675ac400-da4f-4630-9076-b4e64f98b41f');
    });

    it("Should return immediately if item archived already", function () {
        var item = todos[2];
        
        service.archiveTodo(item, todos);

        // Unchanged
        expect(todos.length).toBe(4);
        expect(todos[0].Id).toBe('90685688-9b26-4ce1-82b6-b3652521604f');
        expect(todos[1].Id).toBe('b6d17f69-f40b-4d7e-bd1b-322cdf862efa');
        expect(todos[2].Id).toBe('0124e316-8ace-48d8-bd3a-3e7a2f1dba32');
        expect(todos[3].Id).toBe('675ac400-da4f-4630-9076-b4e64f98b41f');
    });

    it("Should add a todo", function () {
        $httpBackend.expectPOST('/HomeJson/AddTodo');
        var item = todos[0];

        addTodoResponse.respond({
            Archived: false,
            Completed: '',
            Created: 'Wednesday',
            Description: 'new todo',
            ElapsedDaysClass: '',
            Id: '24635783-9b26-4ce1-82b6-b3652521604f'
        })

        service.addTodo('new todo', todos);

        $httpBackend.flush();

        expect(todos.length).toBe(5);
        expect(todos[0].Id).toBe('24635783-9b26-4ce1-82b6-b3652521604f');
        expect(todos[1].Id).toBe('90685688-9b26-4ce1-82b6-b3652521604f');
        expect(todos[2].Id).toBe('b6d17f69-f40b-4d7e-bd1b-322cdf862efa');
        expect(todos[3].Id).toBe('0124e316-8ace-48d8-bd3a-3e7a2f1dba32');
        expect(todos[4].Id).toBe('675ac400-da4f-4630-9076-b4e64f98b41f');
    });
});