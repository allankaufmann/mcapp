/* tslint:disable max-line-length */
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';
import { HttpHeaders, HttpResponse } from '@angular/common/http';

import { McappWebTestModule } from '../../../test.module';
import { QuizComponent } from 'app/entities/quiz/quiz.component';
import { QuizService } from 'app/entities/quiz/quiz.service';
import { Quiz } from 'app/shared/model/quiz.model';

describe('Component Tests', () => {
    describe('Quiz Management Component', () => {
        let comp: QuizComponent;
        let fixture: ComponentFixture<QuizComponent>;
        let service: QuizService;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [QuizComponent],
                providers: []
            })
                .overrideTemplate(QuizComponent, '')
                .compileComponents();

            fixture = TestBed.createComponent(QuizComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(QuizService);
        });

        it('Should call load all on init', () => {
            // GIVEN
            const headers = new HttpHeaders().append('link', 'link;link');
            spyOn(service, 'query').and.returnValue(
                of(
                    new HttpResponse({
                        body: [new Quiz(123)],
                        headers
                    })
                )
            );

            // WHEN
            comp.ngOnInit();

            // THEN
            expect(service.query).toHaveBeenCalled();
            expect(comp.quizzes[0]).toEqual(jasmine.objectContaining({ id: 123 }));
        });
    });
});
