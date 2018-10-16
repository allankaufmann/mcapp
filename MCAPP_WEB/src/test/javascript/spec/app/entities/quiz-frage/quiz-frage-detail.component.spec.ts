/* tslint:disable max-line-length */
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';

import { McappWebTestModule } from '../../../test.module';
import { QuizFrageDetailComponent } from 'app/entities/quiz-frage/quiz-frage-detail.component';
import { QuizFrage } from 'app/shared/model/quiz-frage.model';

describe('Component Tests', () => {
    describe('QuizFrage Management Detail Component', () => {
        let comp: QuizFrageDetailComponent;
        let fixture: ComponentFixture<QuizFrageDetailComponent>;
        const route = ({ data: of({ quizFrage: new QuizFrage(123) }) } as any) as ActivatedRoute;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [QuizFrageDetailComponent],
                providers: [{ provide: ActivatedRoute, useValue: route }]
            })
                .overrideTemplate(QuizFrageDetailComponent, '')
                .compileComponents();
            fixture = TestBed.createComponent(QuizFrageDetailComponent);
            comp = fixture.componentInstance;
        });

        describe('OnInit', () => {
            it('Should call load all on init', () => {
                // GIVEN

                // WHEN
                comp.ngOnInit();

                // THEN
                expect(comp.quizFrage).toEqual(jasmine.objectContaining({ id: 123 }));
            });
        });
    });
});
