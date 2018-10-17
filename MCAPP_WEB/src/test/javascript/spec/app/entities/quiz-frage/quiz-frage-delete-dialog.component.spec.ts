/* tslint:disable max-line-length */
import { ComponentFixture, TestBed, inject, fakeAsync, tick } from '@angular/core/testing';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import { JhiEventManager } from 'ng-jhipster';

import { McappWebTestModule } from '../../../test.module';
import { QuizFrageDeleteDialogComponent } from 'app/entities/quiz-frage/quiz-frage-delete-dialog.component';
import { QuizFrageService } from 'app/entities/quiz-frage/quiz-frage.service';

describe('Component Tests', () => {
    describe('QuizFrage Management Delete Component', () => {
        let comp: QuizFrageDeleteDialogComponent;
        let fixture: ComponentFixture<QuizFrageDeleteDialogComponent>;
        let service: QuizFrageService;
        let mockEventManager: any;
        let mockActiveModal: any;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [QuizFrageDeleteDialogComponent]
            })
                .overrideTemplate(QuizFrageDeleteDialogComponent, '')
                .compileComponents();
            fixture = TestBed.createComponent(QuizFrageDeleteDialogComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(QuizFrageService);
            mockEventManager = fixture.debugElement.injector.get(JhiEventManager);
            mockActiveModal = fixture.debugElement.injector.get(NgbActiveModal);
        });

        describe('confirmDelete', () => {
            it('Should call delete service on confirmDelete', inject(
                [],
                fakeAsync(() => {
                    // GIVEN
                    spyOn(service, 'delete').and.returnValue(of({}));

                    // WHEN
                    comp.confirmDelete(123);
                    tick();

                    // THEN
                    expect(service.delete).toHaveBeenCalledWith(123);
                    expect(mockActiveModal.dismissSpy).toHaveBeenCalled();
                    expect(mockEventManager.broadcastSpy).toHaveBeenCalled();
                })
            ));
        });
    });
});
