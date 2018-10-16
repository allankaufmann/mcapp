/* tslint:disable max-line-length */
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';
import { HttpHeaders, HttpResponse } from '@angular/common/http';

import { McappWebTestModule } from '../../../test.module';
import { QuizFrageComponent } from 'app/entities/quiz-frage/quiz-frage.component';
import { QuizFrageService } from 'app/entities/quiz-frage/quiz-frage.service';
import { QuizFrage } from 'app/shared/model/quiz-frage.model';

describe('Component Tests', () => {
    describe('QuizFrage Management Component', () => {
        let comp: QuizFrageComponent;
        let fixture: ComponentFixture<QuizFrageComponent>;
        let service: QuizFrageService;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [QuizFrageComponent],
                providers: []
            })
                .overrideTemplate(QuizFrageComponent, '')
                .compileComponents();

            fixture = TestBed.createComponent(QuizFrageComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(QuizFrageService);
        });

        it('Should call load all on init', () => {
            // GIVEN
            const headers = new HttpHeaders().append('link', 'link;link');
            spyOn(service, 'query').and.returnValue(
                of(
                    new HttpResponse({
                        body: [new QuizFrage(123)],
                        headers
                    })
                )
            );

            // WHEN
            comp.ngOnInit();

            // THEN
            expect(service.query).toHaveBeenCalled();
            expect(comp.quizFrages[0]).toEqual(jasmine.objectContaining({ id: 123 }));
        });
    });
});
